using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{

    public class Stock
    {

        public int Id { get; set; }
        
        [Display(Name = "Остаток на складе")]
        public int Quantity { get; set; }

        [Display(Name = "Товар")]
        public Product Product { get; set; }

        [Display(Name = "Товар")]
        public int ProductId { get; set; }

    }

}