using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models;

public class Goods
{
    public int Id { get; set; }
    
    [Display(Name = "Наименование")]
    public string? Name { get; set; }

    [Display(Name = "Количество на складе")]
    public int Quantity { get; set; }

    [Display(Name = "Последнее поступление")]
    public DateTime LatestArrival { get; set; }

    [Display(Name = "Цена")]
    public decimal Price { get; set; }
}