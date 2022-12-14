using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Services
{
    public class BuggyDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
	        optionsBuilder.UseSqlite("Data Source=products.db");
	        optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
