using Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

    }
}
