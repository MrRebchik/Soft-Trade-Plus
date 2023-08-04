using Microsoft.EntityFrameworkCore;

namespace SoftTradePlus.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SoftTradePlusDB;Trusted_Connection=True;");
        }
    }
}
