using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevelopmentCenter.Core.Models;
using DevelopmentCenter.Infrastructure;

namespace DevelopmentCenter.Core.Repositories
{
    public class AppDbContext : DbContext, IDependencyPerRequest
    {
        public AppDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Article> Articles { get; set; }
    }
}
