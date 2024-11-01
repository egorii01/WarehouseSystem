using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{

    //модель, описывающая товар
    public class Product 
    {
        
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Цена за единицу")]
        public decimal Price { get; set; }

        public bool Actual { get; set; } = true;

    }
}

