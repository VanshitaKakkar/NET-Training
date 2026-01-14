using Entity_Framework_Using_DataBase_approach.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Category> Categories { get; set; }

   
    }
}
