namespace WarehouseSystem.Models
{

    public class Check
    {

        public int Id { get; set; }

        public Employee Cashier { get; set; }

        public DateTime Time { get; set; }

        public decimal GeneralAmount { get; set; }

        public ICollection<CheckEntry> CheckEntries { get; set; }

        public bool Actual { get; set; }

    }

}