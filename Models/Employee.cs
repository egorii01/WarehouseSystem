using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{

    public class Employee
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Должность")]
        public Position Position { get; set; }

    }

}