namespace HIMAIKFinanceMonitorApp.Server.Dtos
{
    public class TransactionDto
    {
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Notes { get; set; } = "";
    }
}
