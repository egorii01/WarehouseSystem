using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int CheckId { get; set; }

        [NotMapped]
        public int MaxQuantity { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Amount 
        {
            get 
            { 
                if (Product == null)
                {
                    return 0;
                }

                return Product.Price * Quantity; 
            }
        }

    }

}