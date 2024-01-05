using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{

    public class Employee
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Должность")]
        public int PositionID { get; set; }

        public bool? Actual { get; set; }

        [Display(Name = "Должность")]
        public Position? Position { get; set; }

    }

}