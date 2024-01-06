using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{

    //поступление
    public class Import
    {

        public int Id { get; set; }

        [Display(Name = "Товар")]
        public int ProductID { get; set; }

        [Display(Name = "Товар")]
        public Product? Product { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public int InvoiceID { get; set; }

    }

}