namespace HIMAIKFinanceMonitorApp.Server.Dtos
{
    public class IncomeDataDto
    {
        public string Name { get; set; } = "";
        public decimal Nominal { get; set; }
        public DateTime TransferDate { get; set; }
        public TransactionDto Transaction { get; set; } = new TransactionDto();
    }
}
