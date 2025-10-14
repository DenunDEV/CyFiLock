namespace CyFiLock.Utils
{

    /// Utilitários para manipulação do console

    public static class ConsoleHelper
    {

        /// Exibe cabeçalho estilizado

        public static void ShowHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║             CyFi Lock v0.1             ║");
            Console.WriteLine("║    Sistema Corporativo de Acesso       ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }


        /// Exibe informações do usuário

        public static void ShowUserInfo(string name, string employeeId, string department)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Usuário: {name}");
            Console.WriteLine($"ID: {employeeId} | Departamento: {department}");
            Console.ResetColor();
            Console.WriteLine(new string('─', 40));
        }

        /// Pausa a execução aguardando entrada do usuário

        public static void WaitForUser()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }


        /// Exibe informações completas do usuário

        public static void ShowUserInfo(string username, string fullName, string employeeId, string department)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Usuário: {username}");
            Console.WriteLine($"Nome Completo: {fullName}");
            Console.WriteLine($"ID: {employeeId} | Departamento: {department}");
            Console.ResetColor();
            Console.WriteLine(new string('─', 50));
        }
    }
}

