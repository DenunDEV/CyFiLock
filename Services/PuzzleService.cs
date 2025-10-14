using System.Text;

namespace CyFiLock.Services
{
    /// <summary>
    /// Serviço responsável pelos CAPTCHAs de autenticação
    /// </summary>
    public class PuzzleService
    {
        private Random _random;
        private string[] _captchaWords = {
            "SEGURANCA", "ACESSO", "SISTEMA", "CYBER", "PROTEÇÃO",
            "DADOS", "SENHA", "CONTA", "VERIFICACAO", "AUTENTICACAO",
            "FIREWALL", "CRIPTOGRAFIA", "PRIVACIDADE", "REDE", "SERVER"
        };

        public PuzzleService()
        {
            _random = new Random();
        }

        /// <summary>
        /// Executa um CAPTCHA aleatório
        /// </summary>
        public (bool success, string puzzleType, TimeSpan timeSpent) ExecuteRandomCaptcha()
        {
            var startTime = DateTime.Now;
            var captchaType = "";
            bool success = false;

            // Seleciona CAPTCHA aleatório
            int captchaNumber = _random.Next(1, 5);

            switch (captchaNumber)
            {
                case 1:
                    captchaType = "TextCaptcha";
                    success = TextCaptchaPuzzle();
                    break;
                case 2:
                    captchaType = "MathCaptcha";
                    success = MathCaptchaPuzzle();
                    break;
                case 3:
                    captchaType = "SequenceCaptcha";
                    success = SequenceCaptchaPuzzle();
                    break;
                case 4:
                    captchaType = "ImageCaptcha";
                    success = ImageCaptchaPuzzle();
                    break;
            }

            var timeSpent = DateTime.Now - startTime;
            return (success, captchaType, timeSpent);
        }

        /// <summary>
        /// CAPTCHA de texto distorcido
        /// </summary>
        private bool TextCaptchaPuzzle()
        {
            Console.Clear();
            Console.WriteLine("=== VERIFICAÇÃO DE SEGURANÇA: CAPTCHA DE TEXTO ===\n");

            string word = _captchaWords[_random.Next(_captchaWords.Length)];
            string distortedWord = ApplyTextDistortion(word);

            Console.WriteLine("Digite o texto que aparece abaixo:\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔{new string('═', distortedWord.Length + 2)}╗");
            Console.WriteLine($"║ {distortedWord}                           ║");
            Console.WriteLine($"╚{new string('═', distortedWord.Length + 2)}╝");
            Console.ResetColor();
            Console.WriteLine("\nTexto (em maiúsculas, sem espaços):");

            string input = Console.ReadLine()?.Trim().ToUpper() ?? "";
            return input == word;
        }

        /// <summary>
        /// Aplica distorção ao texto do CAPTCHA
        /// </summary>
        private string ApplyTextDistortion(string text)
        {
            StringBuilder distorted = new StringBuilder();
            foreach (char c in text)
            {
                // Aplica efeitos aleatórios de distorção
                switch (_random.Next(4))
                {
                    case 0:
                        distorted.Append(char.ToLower(c)); // Mistura maiúsculas e minúsculas
                        break;
                    case 1:
                        distorted.Append(c);
                        distorted.Append((char)_random.Next(33, 48)); // Caracteres especiais
                        break;
                    default:
                        distorted.Append(c);
                        break;
                }
            }
            return distorted.ToString();
        }

        /// <summary>
        /// CAPTCHA matemático com operações simples
        /// </summary>
        private bool MathCaptchaPuzzle()
        {
            Console.Clear();
            Console.WriteLine("=== VERIFICAÇÃO DE SEGURANÇA: CAPTCHA MATEMÁTICO ===\n");

            int a = _random.Next(1, 20);
            int b = _random.Next(1, 20);
            string[] operators = { "+", "-", "*" };
            string op = operators[_random.Next(operators.Length)];

            int correctAnswer = op switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                _ => a + b
            };

            // Aplica "ruído" visual
            string[] noise = { "~~", "//", "\\\\", "||", "==" };
            string noiseLine = noise[_random.Next(noise.Length)];

            Console.WriteLine("Resolva a expressão matemática:\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{noiseLine}{a} {op} {b}{noiseLine}");
            Console.ResetColor();
            Console.WriteLine("\nResposta:");

            return int.TryParse(Console.ReadLine(), out int answer) && answer == correctAnswer;
        }

        /// <summary>
        /// CAPTCHA de sequência lógica
        /// </summary>
        private bool SequenceCaptchaPuzzle()
        {
            Console.Clear();
            Console.WriteLine("=== VERIFICAÇÃO DE SEGURANÇA: CAPTCHA DE SEQUÊNCIA ===\n");

            // Gera sequência com padrão específico
            int start = _random.Next(1, 10);
            int pattern = _random.Next(1, 4);

            string sequence = "";
            int nextNumber = start;
            int correctAnswer = 0;

            for (int i = 0; i < 4; i++)
            {
                sequence += $"{nextNumber} ";

                switch (pattern)
                {
                    case 1: // Soma constante
                        nextNumber += 2;
                        correctAnswer = nextNumber;
                        break;
                    case 2: // Multiplicação
                        nextNumber *= 2;
                        correctAnswer = nextNumber;
                        break;
                    case 3: // Sequência alternada
                        nextNumber = i % 2 == 0 ? nextNumber + 1 : nextNumber + 3;
                        correctAnswer = nextNumber;
                        break;
                }
            }

            Console.WriteLine("Complete a sequência lógica:\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{sequence}?");
            Console.ResetColor();
            Console.WriteLine("\nPróximo número:");

            return int.TryParse(Console.ReadLine(), out int answer) && answer == correctAnswer;
        }

        /// CAPTCHA baseado em reconhecimento de padrão ASCII
        private bool ImageCaptchaPuzzle()
        {
            Console.Clear();
            Console.WriteLine("=== VERIFICAÇÃO DE SEGURANÇA: CAPTCHA VISUAL ===\n");

            string[] patterns = {
                """
                ████████
                ██    ██
                ████████
                ██    ██
                ████████
                """,
                """
                ██  ████
                ████  ██
                ██    ██
                ████████
                ██    ██
                """,
                """
                 ███████
                ██     ██
                ██    
                ██     ██
                 ███████
                """
            };

            string[] patternNames = { "RETÂNGULO", "LETRA A", "CÍRCULO" };
            int patternIndex = _random.Next(patterns.Length);
            string selectedPattern = patterns[patternIndex];
            string correctAnswer = patternNames[patternIndex];

            Console.WriteLine("Qual forma geométrica é representada abaixo?\n");
            Console.ForegroundColor = ConsoleColor.Magenta;

            // Aplica "glitches" visuais no padrão
            string[] glitchedPattern = ApplyVisualGlitches(selectedPattern);
            foreach (string line in glitchedPattern)
            {
                Console.WriteLine(line);
            }

            Console.ResetColor();
            Console.WriteLine("\nOpções: RETÂNGULO, LETRA A, CÍRCULO");
            Console.WriteLine("Resposta:");

            string input = Console.ReadLine()?.Trim().ToUpper() ?? "";
            return input == correctAnswer;
        }

        /// Aplica glitches visuais ao padrão ASCII
       
        private string[] ApplyVisualGlitches(string pattern)
        {
            string[] lines = pattern.Split('\n');
            string[] glitchedLines = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                char[] chars = lines[i].ToCharArray();

                // Aplica glitches aleatórios em algumas posições
                for (int j = 0; j < chars.Length; j++)
                {
                    if (_random.Next(100) < 15) // 15% de chance de glitch
                    {
                        chars[j] = _random.Next(2) == 0 ? '░' : '▒';
                    }
                }

                glitchedLines[i] = new string(chars);
            }

            return glitchedLines;
        }

        /// Gera um CAPTCHA com tempo limite
        public (bool success, string puzzleType, TimeSpan timeSpent) ExecuteTimedCaptcha(int timeLimitSeconds = 30)
        {
            var startTime = DateTime.Now;
            var result = ExecuteRandomCaptcha();
            var timeSpent = DateTime.Now - startTime;

            if (timeSpent.TotalSeconds > timeLimitSeconds)
            {
                return (false, $"{result.puzzleType}_TIMEOUT", timeSpent);
            }

            return result;
        }
    }
}