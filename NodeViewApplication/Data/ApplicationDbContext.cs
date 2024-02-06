using Microsoft.EntityFrameworkCore;
using NodeViewApplication.Models;
using System.Collections.Generic;

namespace NodeViewApplication.Data
{
    public class ApplicationDbContext :  DbContext { 
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }

    }
}
