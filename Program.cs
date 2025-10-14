using CyFiLock.Models;
using CyFiLock.Services;
using CyFiLock.Effects;
using CyFiLock.Utils;

namespace CyFiLock
{
    class Program
    {
        private static AuthenticationService _authService;
        private static PuzzleService _puzzleService;

        static void Main(string[] args)
        {
            InitializeServices();
            RunApplication();
        }

        /// Inicializa os serviços do sistema
        /// 
        private static void InitializeServices()
        {
            _authService = new AuthenticationService();
            _puzzleService = new PuzzleService();
        }


        /// Executa o fluxo principal da aplicação

        private static void RunApplication()
        {
            while (true)
            {
                ConsoleHelper.ShowHeader();

                // Etapa 1: Login básico
                var user = LoginStage();
                if (user == null) continue;

                // Etapa 2: Registro de acesso
                AccessRegistrationStage(user);

                // Etapa 3: Puzzle de autenticação
                if (!user.IsMasterKey)
                {
                    bool puzzleSuccess = PuzzleStage(user);
                    if (!puzzleSuccess) return; // Sai do programa se falhar no puzzle
                }

                // Etapa 4: Confirmação final
                FinalConfirmationStage(user);

                break;
            }
        }

        /// Gerencia a etapa de login com credenciais completas
        private static User LoginStage()
        {
            Console.WriteLine("=== ETAPA 1: AUTENTICAÇÃO SEGURA ===\n");

            Console.Write("Login: ");
            string UserName = Console.ReadLine() ?? "";

            Console.Write("Digite Seu ID: ");
            string employeeId = Console.ReadLine() ?? "";

            Console.Write("Senha Requerida: ");
            string password = SecurityHelper.ReadPassword();

            var (user, error) = _authService.AuthenticateUser(UserName, employeeId, password);

            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nErro: {error}");
                Console.ResetColor();

                // Log da tentativa falha
                _authService.LogAccess(employeeId, false, "Authentication", TimeSpan.Zero, $"Falha: {error}");

                ConsoleHelper.WaitForUser();
                return null;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ Credenciais válidas para {user.Username}");
            Console.ResetColor();

            return user;
        }

        /// Gerencia o registro de acesso
        private static void AccessRegistrationStage(User user)
        {
            ConsoleHelper.ShowHeader();
            ConsoleHelper.ShowUserInfo(user.Username, user.EmployeeId, user.Department);

            Console.WriteLine("=== ETAPA 2: REGISTRO DE ACESSO CORPORATIVO ===\n");

            DateTime accessTime = DateTime.Now;
            Console.WriteLine($"Data/Hora de Acesso: {accessTime:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"Usuário: {user.Username}");
            //Console.WriteLine($"Nome Completo: {user.FullName}");
            Console.WriteLine($"Departamento: {user.Department}");
            Console.WriteLine($"ID do Funcionário: {user.EmployeeId}");

            if (user.IsMasterKey)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n PRIVILÉGIO DE CHAVE MESTRA DETECTADO ");
                Console.ResetColor();
                Console.WriteLine("Acesso direto permitido");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n ACESSO PADRÃO - Verificação de segurança necessária");
                Console.ResetColor();
            }

            ConsoleHelper.WaitForUser();
        }

        /// Gerencia a etapa de CAPTCHA
        private static bool PuzzleStage(User user)
        {
            ConsoleHelper.ShowHeader();
            ConsoleHelper.ShowUserInfo(user.Username, user.EmployeeId, user.Department);

            Console.WriteLine("=== ETAPA 3: VERIFICAÇÃO CAPTCHA ===\n");

            GlitchEffect.TypeWriterEffect("Inicializando sistema de verificação CAPTCHA...");
            GlitchEffect.TypeWriterEffect("Carregando... Espere, por favor.");
            Thread.Sleep(1500);

            Console.WriteLine(" Sistema de segurança CAPTCHA ativado ");
            Console.WriteLine(" Resposta rápida aumenta a segurança do sistema \n");

            // Adiciona pequeno delay para efeito
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            Console.WriteLine("\n");

            var (success, puzzleType, timeSpent) = _puzzleService.ExecuteRandomCaptcha();
            _authService.LogAccess(user.EmployeeId, success, puzzleType, timeSpent,
                                  $"Tempo: {timeSpent.TotalSeconds:F1}s");

            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n CAPTCHA RESOLVIDO COM SUCESSO! ");
                Console.WriteLine($" Tempo: {timeSpent.TotalSeconds:F1} segundos");
                Console.ResetColor();

                // Efeito de glitch de sucesso
                GlitchEffect.ShowSuccessGlitch();

                ConsoleHelper.WaitForUser();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n FALHA NA VERIFICAÇÃO CAPTCHA ");
                Console.WriteLine($" Tempo: {timeSpent.TotalSeconds:F1} segundos");
                Console.ResetColor();

                Console.WriteLine("Ativando protocolo de segurança...");
                Thread.Sleep(3000);

                GlitchEffect.ShowErrorGlitchAndExit("Falha na verificação CAPTCHA");
                return false;
            }
        }

        /// Exibe o histórico de acessos
        private static void ShowAccessHistory()
        {
            Console.WriteLine("\n" + new string('═', 70));
            Console.WriteLine(" HISTÓRICO DE ACESSOS E VERIFICAÇÕES CAPTCHA ");
            Console.WriteLine(new string('═', 70));

            var history = _authService.GetAccessHistory().Take(8);

            foreach (var log in history)
            {
                string status = log.Success ? " SUCESSO " : " FALHA ";
                string time = log.AccessTime.ToString("dd/MM HH:mm");
                string captchaType = log.PuzzleType.Replace("Captcha", "");

                Console.ForegroundColor = log.Success ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write($"{time} - {log.EmployeeId} - {captchaType}".PadRight(35));
                Console.ResetColor();
                Console.Write($" - {status}".PadRight(15));

                if (!string.IsNullOrEmpty(log.AdditionalInfo))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" [{log.AdditionalInfo}]");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        /// Gerencia a confirmação final
        private static void FinalConfirmationStage(User user)
        {
            ConsoleHelper.ShowHeader();
            ConsoleHelper.ShowUserInfo(user.Username, user.EmployeeId, user.Department);

            Console.WriteLine("=== CONFIRMAÇÃO FINAL DE IDENTIDADE ===\n");
            Console.WriteLine("Por segurança, digite seu NOME COMPLETO para confirmar:");
            Console.Write(">> ");

            string confirmation = Console.ReadLine() ?? "";

            if (confirmation.Trim().Equals(user.FullName, StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n IDENTIDADE CONFIRMADA! BEM-VINDO(A), {user.FullName.ToUpper()}!");
                Console.ResetColor();
                Console.WriteLine("Acesso completo ao sistema corporativo concedido.");

                // Log de acesso bem-sucedido
                _authService.LogAccess(user.EmployeeId, true, "FinalConfirmation", TimeSpan.Zero, "Acesso Concedido");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n FALHA NA CONFIRMAÇÃO DE IDENTIDADE! ");
                Console.ResetColor();
                Console.WriteLine("Acesso revogado por medidas de segurança.");

                // Log de falha na confirmação
                _authService.LogAccess(user.EmployeeId, false, "FinalConfirmation", TimeSpan.Zero, "Falha na confirmação de identidade");
            }

            ShowAccessHistory();
            ConsoleHelper.WaitForUser();
        }
    }
}