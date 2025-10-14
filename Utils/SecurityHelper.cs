namespace CyFiLock.Utils
{
    /// Utilitários de segurança para entrada de dados sensíveis
    public static class SecurityHelper
    {
        /// Lê senha do console sem exibir os caracteres
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace não deve funcionar
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        /// Verifica força da senha (para futuras implementações)
        public static bool IsPasswordStrong(string password)
        {
            // Implementação básica - pode ser expandida
            return password.Length >= 6 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit);
        }

        /// Exibe requisitos de senha

        public static void ShowPasswordRequirements()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Requisitos de senha:");
            Console.WriteLine("- Mínimo 6 caracteres");
            Console.WriteLine("- Pelo menos uma letra maiúscula");
            Console.WriteLine("- Pelo menos uma letra minúscula");
            Console.WriteLine("- Pelo menos um número");
            Console.ResetColor();
        }
    }
}