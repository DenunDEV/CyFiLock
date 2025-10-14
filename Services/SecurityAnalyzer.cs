using CyFiLock.Models;

namespace CyFiLock.Services
{
    /// Analisa padrões de segurança e comportamento do usuário
    public class SecurityAnalyzer
    {
        private List<AccessLog> _accessLogs;

        public SecurityAnalyzer(List<AccessLog> accessLogs)
        {
            _accessLogs = accessLogs;
        }

        /// Calcula score de segurança baseado no tempo de resposta
        public double CalculateSecurityScore(TimeSpan responseTime, string captchaType)
        {
            double baseScore = 100;
            double timePenalty = Math.Min(responseTime.TotalSeconds * 2, 50); // Penalidade por demora

            // Bônus por tipos de CAPTCHA mais complexos
            double complexityBonus = captchaType switch
            {
                "ImageCaptcha" => 10,
                "SequenceCaptcha" => 5,
                _ => 0
            };

            return Math.Max(baseScore - timePenalty + complexityBonus, 0);
        }

        /// Verifica se há comportamento suspeito
        public bool DetectSuspiciousBehavior(string employeeId)
        {
            var userLogs = _accessLogs
                .Where(log => log.EmployeeId == employeeId)
                .OrderByDescending(log => log.AccessTime)
                .Take(5)
                .ToList();

            if (userLogs.Count < 3) return false;

            // Suspeita se muitas falhas recentes
            int recentFailures = userLogs.Count(log => !log.Success);
            return recentFailures >= 3;
        }
    }
}