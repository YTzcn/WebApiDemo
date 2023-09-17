using Microsoft.EntityFrameworkCore;
using Yahya.WebApiDemo.Entities;

namespace Yahya.WebApiDemo.DataAcces
{
    public class NorthwindContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=Northwind;Integrated Security=true;TrustServerCertificate=True;");
        }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Category> Categories{ get; set; }
    }
}
