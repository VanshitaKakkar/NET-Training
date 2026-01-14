using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scaffolding.Models;

namespace Scaffolding.Data
{
    public class MVC_DbContext : DbContext
    {
        public MVC_DbContext (DbContextOptions<MVC_DbContext> options)
            : base(options)
        {
        }

        public DbSet<Scaffolding.Models.Category> Category { get; set; } = default!;
    }
}
