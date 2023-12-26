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

        //Метод создания таблиц в БД с кастомными названиями
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Check>().ToTable("Check");
            modelBuilder.Entity<CheckEntry>().ToTable("CheckEntry");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Import>().ToTable("Import");
            modelBuilder.Entity<Invoice>().ToTable("Invoice");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Stock>().ToTable("Stock");
        }
    }

}