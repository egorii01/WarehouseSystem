namespace WarehouseSystem.Models
{
    //фактура о поступлении
    public class Invoice
    {

        public int Id { get; set; }

        public DateTime Time { get; set; }

        public Employee Responsible { get; set; }

        public ICollection<Import> Imports { get; set; }

    }

}