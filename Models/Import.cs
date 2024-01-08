using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey(nameof(Invoice))]
        public int? InvoiceID { get; set; }
        public Invoice Invoice { get; set; }

        public Import()
        {

        }

        public Import(Invoice invoice)
        {
            Invoice = invoice;
        }

    }

}