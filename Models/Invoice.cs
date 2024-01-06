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

        public ICollection<Import> Imports { get; set; }

    }

}