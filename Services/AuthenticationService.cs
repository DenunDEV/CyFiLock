using CyFiLock.Models;

namespace CyFiLock.Services
{
    /// Serviço responsável pela autenticação de usuários com verificação completa
    public class AuthenticationService
    {
        private List<User> _users;
        private List<AccessLog> _accessLogs;

        public AuthenticationService()
        {
            _users = new List<User>();
            _accessLogs = new List<AccessLog>();
            InitializeSampleUsers();
        }

        private void InitializeSampleUsers()
        {
            // Usuários de exemplo - Em produção, usar banco de dados com senhas hasheadas
            _users.Add(new User("Ast.Fonseca", "Astolpho Assunção da Fonseca", "AS001", "AAF@789", "TI", true));
            _users.Add(new User("Carlos.Ara", "Carlos Araujo Dos Santos", "CS002", "Senha@456", "RH"));
            _users.Add(new User("Marina.Fer", "Marina Ferreira De Oliveira", "MO003", "Senha@789", "Financeiro"));
            _users.Add(new User("Joao.prr", "João Carlos Pereira", "JC004", "Senha@101", "Operações"));
        }

        /// Autentica usuário com nome completo, ID e senha

        public (User user, string error) AuthenticateUser(string UserName, string employeeId, string password)
        {
            var user = _users.FirstOrDefault(u =>
                u.Username.Equals(UserName, StringComparison.OrdinalIgnoreCase) &&
                u.EmployeeId == employeeId);

            if (user == null)
            {
                return (null, "Usuário não encontrado. Verifique nome completo e ID.");
            }

            if (user.IsLocked)
            {
                return (null, "Conta bloqueada devido a múltiplas tentativas falhas. Contate o administrador.");
            }

            if (!user.VerifyPassword(password))
            {
                user.RecordFailedAttempt();
                return (null, $"Senha incorreta. Tentativas restantes: {3 - user.FailedAttempts}");
            }

            // Login bem-sucedido
            user.ResetFailedAttempts();
            return (user, null);
        }

        /// Busca usuário apenas pelo ID (para confirmação final)
        public User GetUserById(string employeeId)
        {
            return _users.FirstOrDefault(u => u.EmployeeId == employeeId);
        }


        /// Registra tentativa de acesso
        public void LogAccess(string employeeId, bool success, string puzzleType, TimeSpan timeSpent, string additionalInfo = "")
        {
            _accessLogs.Add(new AccessLog(employeeId, success, puzzleType, timeSpent)
            {
                AdditionalInfo = additionalInfo
            });
        }

        /// Obtém histórico de acessos
        public List<AccessLog> GetAccessHistory()
        {
            return _accessLogs.OrderByDescending(a => a.AccessTime).ToList();
        }
    }
}