using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{
    //фактура о поступлении
    public class Invoice
    {
        
        [Display(Name = "№")]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата создания")]
        public DateTime Time { get; set; }

        [Display(Name = "ФИО ответственного")]
        public int ResponsibleID { get; set; }

        [Display(Name = "ФИО ответственного")]
        public Employee Responsible { get; set; }

        [Display(Name = "Пришедшие товары")]
        public ICollection<Import> Imports { get; set; }

    }

}