using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.core.Data;


namespace web.core.Models
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public  DbSet<Car>Cars{ set; get; }
        public DbSet<Driver> Drivers { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Rental> Rentals { set; get; }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {     //make Number of car unique
            modelBuilder.Entity<Car>()
            .HasIndex(c => c.Number)
            .IsUnique();

         //make CreatedAt field  auto in Rental model

            modelBuilder.Entity<Rental>()
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("getdate()");
        }


    }
}
