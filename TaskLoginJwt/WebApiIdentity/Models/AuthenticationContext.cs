
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiIdentity.Models.Company;
using WebApiIdentity.Models.MasterDetalModel;

namespace WebApiIdentity.Models
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }
       // we can configure below if we want our connection string here
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-O84LAFN;Database=OrgAPIDb;Trusted_Connection=True;");
        //}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //code for Product
        //public DbSet<Product> Products { get; set; }
        //public DbSet<OrderProduct> orderProducts { get; set; }
        //public DbSet<Details> Details { get; set; }
        //public DbSet<Item> Item { get; set; }
        //public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Details>().HasKey(x => new { x.ItemId, x.OrderId });

        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<OrderProduct>().HasKey(x => new { x.ProductId, x.OrderId });

        //}


    }
}
