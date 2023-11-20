using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence
{

    /* The DbCtx class is a DbContext implementation in C# that connects to a PostgreSQL database and
  defines the relationships  */
    public class DbCtx : IdentityDbContext<User>{
        protected readonly IConfiguration Configuration;
        public DbCtx(DbContextOptions options, IConfiguration configuration) : base(options){
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnection"),b=>{
                b.MigrationsAssembly("Persistence");
            });
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>()
            .HasMany(c=> c.Products)
            .WithOne(p=> p.Category)
            .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Category> Categories {get; set;}
        public DbSet<Product> Products {get; set;}
    }
}