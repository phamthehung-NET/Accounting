namespace Accounting.Model.DTO
{
    public class DebtDashboardDTO
    {
        public string Name { get; set; }

        public decimal Debt { get; set; }

        public DateTime Date { get; set; }

        public string LunarDate { get; set; }
    }
}
