namespace CyFiLock.Models
{
    /// Registro de acesso ao sistema com informações detalhadas
    public class AccessLog
    {
        public string EmployeeId { get; set; }
        public DateTime AccessTime { get; set; }
        public bool Success { get; set; }
        public string PuzzleType { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public string AdditionalInfo { get; set; }

        public AccessLog(string employeeId, bool success, string puzzleType, TimeSpan timeSpent)
        {
            EmployeeId = employeeId;
            AccessTime = DateTime.Now;
            Success = success;
            PuzzleType = puzzleType;
            TimeSpent = timeSpent;
            AdditionalInfo = string.Empty;
        }
    }
}