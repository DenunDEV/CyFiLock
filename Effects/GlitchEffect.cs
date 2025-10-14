using System.Text;

namespace CyFiLock.Effects
{

    /// Gerencia efeitos visuais de glitch

    public static class GlitchEffect
    {
        private static Random _random = new Random();

        /// Exibe efeito de glitch de sucesso (ciano)

        public static void ShowSuccessGlitch()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            for (int i = 0; i < 10; i++)
            {
                GenerateGlitchScreen("■_V□●■_E○■_♠R○♠_I○♠♣F♥○♠○♠I♦▼◄ª▼◄C○♠*_A▲N►D_°O○♠▼◄", ConsoleColor.Cyan);
                Thread.Sleep(100);
            }

            Console.ResetColor();
            Console.Clear();
        }

        /// Exibe efeito de glitch de erro (vermelho) com contagem regressiva

        public static void ShowErrorGlitchAndExit(string reason = "Violação de segurança")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;

            for (int i = 10; i > 0; i--) // 10 segundos com contagem regressiva
            {
                Console.Clear();
                GenerateGlitchScreen("╬╩╦L╗╝╚║B╣╗╠═O║╩U╦║╦╩╩C║╩╦║╩S╦║╩╦╬A║║C║║L║╩╦╠║╩╦╠I║╣╗║Z╗╝╚║╣╗A╠║╩╠║╩Ç╣╗╝╣╗╝Ã╦║╦╩╦║╦╩O═╣╗╦║╦╩╝╚R╩╦╠═A║╩╦╠═╣╗╝╚S╔╣╗╝╦╠T═╩╦║╦╩╦R╠╩╦╠O╠══╣╗╝╚║╣╗╝╚╔", ConsoleColor.DarkRed);

                // Mensagem de erro
                Console.SetCursorPosition(10, 5);
                Console.WriteLine($"VIOLAÇÃO DE SEGURANÇA DETECTADA");
                Console.SetCursorPosition(10, 6);
                Console.WriteLine($"Motivo: {reason}");
                Console.SetCursorPosition(10, 8);
                Console.WriteLine($"Sistema será encerrado em: {i}s");

                Thread.Sleep(1000);
            }

            Environment.Exit(0);
        }

        /// Gera uma tela de glitch com caracteres aleatórios

        private static void GenerateGlitchScreen(string characters, ConsoleColor color)
        {
            Console.Clear();
            StringBuilder glitchText = new StringBuilder();

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (_random.Next(100) < 30) // 30% de chance de glitch
                    {
                        char randomChar = characters[_random.Next(characters.Length)];
                        glitchText.Append(randomChar);
                    }
                    else
                    {
                        glitchText.Append(' ');
                    }
                }
                glitchText.Append('\n');
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(glitchText.ToString());
        }

        /// Exibe mensagem com efeito de digitação

        public static void TypeWriterEffect(string text, int delay = 50)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public static void ShowCaptchaWarning()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            string[] warningLines = {
            "╔════════════════════════════════════════╗",
            "║          VERIFICAÇÃO CAPTCHA           ║",
            "║        SISTEMA DE SEGURANÇA            ║",
            "╚════════════════════════════════════════╝"
            };

            foreach (string line in warningLines)
            {
                Console.WriteLine(line);
                Thread.Sleep(200);
            }

            Console.ResetColor();
            Thread.Sleep(1000);
            Console.Clear();
        }

    }
}