using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{

    public class Check
    {

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Кассир")]
        public Employee Cashier { get; set; }

        [Display(Name = "Кассир")]
        public int CashierID { get; set; }

        [Display(Name = "Дата и время создания")]
        public DateTime Time { get; set; }

        [Display(Name = "Стоимость")]
        public decimal GeneralAmount 
        {
            get 
            { 
                decimal result = 0;

                foreach (CheckEntry entry in CheckEntries)
                {
                    result += entry.Amount;
                }
                return result;
            }
        }

        [Display(Name = "Содержимое чека")]
        public ICollection<CheckEntry> CheckEntries { get; set; }

        public bool Actual { get; set; }

    }

}