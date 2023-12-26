namespace WarehouseSystem.Models
{

    public class CheckEntry
    {

        public int Id { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Amount 
        {
            get { return Product.Price * Quantity; }
        }

    }

}