using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{

    public class CheckEntry
    {

        public int Id { get; set; }

        [Display(Name = "Товар")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Amount 
        {
            get { return Product.Price * Quantity; }
        }

    }

}