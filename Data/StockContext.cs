using WarehouseSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace WarehouseSystem.Data
{

    public class StockContext : DbContext
    {

        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {
        }

        //перечисляем множества данных из моделей в контексте БД
        public DbSet<Check> Checks { get; set; }
        public DbSet<CheckEntry> CheckEntries { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> StockRecords { get; set; }
    }

}