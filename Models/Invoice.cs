using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseSystem.Controllers.ImportsController;

namespace WarehouseSystem.Models
{
    //фактура о поступлении
    public class Invoice
    {
        
        [Display(Name = "№")]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата создания")]
        public DateTime? Time { get; set; }

        [Display(Name = "ФИО ответственного")]
        public int? ResponsibleID { get; set; }

        [Display(Name = "ФИО ответственного")]
        public Employee? Responsible { get; set; }

        [Display(Name = "Пришедшие товары")]
        public ICollection<Import> Imports { get; set; }

        public bool Saved { get; set; }

        public Invoice()
        {
            
            Imports = new List<Import>();
            Saved = false;
            Time = DateTime.Now;
            Responsible = null;
            ResponsibleID = null;

        }

        public void ClearData()
        {
            Imports = new List<Import>();
            Saved = false;
            Time = DateTime.Now;
            Responsible = null;
            ResponsibleID = null;
        }

    }

}