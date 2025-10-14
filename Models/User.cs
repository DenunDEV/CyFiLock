namespace CyFiLock.Models
{
    /// Representa um usuário do sistema com informações completas
    public class User
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsMasterKey { get; set; }
        public int FailedAttempts { get; set; }
        public bool IsLocked { get; set; }

        public User(string username, string fullName, string employeeId, string password, string department, bool isMasterKey = false)
        {
            Username = username;
            FullName = fullName;
            EmployeeId = employeeId;
            Password = password;
            Department = department;
            CreatedAt = DateTime.Now;
            IsMasterKey = isMasterKey;
            FailedAttempts = 0;
            IsLocked = false;
        }

        /// Verifica se a senha fornecida corresponde à senha do usuário

        public bool VerifyPassword(string inputPassword)
        {
            return Password == inputPassword;
        }

        /// Incrementa tentativas falhas e bloqueia se necessário

        public void RecordFailedAttempt()
        {
            FailedAttempts++;
            if (FailedAttempts >= 3)
            {
                IsLocked = true;
            }
        }

        /// Reseta tentativas falhas após login bem-sucedido

        public void ResetFailedAttempts()
        {
            FailedAttempts = 0;
            IsLocked = false;
        }
    }
}