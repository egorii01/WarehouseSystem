using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{
    //фактура о поступлении
    public class Invoice
    {
        
        [Display(Name = "ID")]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Время создания")]
        public DateTime Time { get; set; }

        [Display(Name = "ФИО ответственного")]
        public int ResponsibleID { get; set; }

        [Display(Name = "ФИО ответственного")]
        public Employee Responsible { get; set; }

        [Display(Name = "Список поступивших товаров")]
        public ICollection<Import> Imports { get; set; }

        public bool Actual { get; set; } = true;

        public Invoice()
        {
            Imports = new List<Import>();
        }

    }

}