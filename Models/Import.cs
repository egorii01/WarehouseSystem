namespace WarehouseSystem.Models
{

    //поступление
    public class Import
    {

        public int Id { get; set; }

        public int ProductID { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public int InvoiceID { get; set; }

    }

}