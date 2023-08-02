using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.core.Entities;
using web.infrastructure.Configuration;


namespace web.infrastructure.Data
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
        {     
            base.OnModelCreating(modelBuilder);
            new CarEntityTypeConfiguration().Configure(modelBuilder.Entity<Car>());
            new RentalEntityTypeConfiguration().Configure(modelBuilder.Entity<Rental>());
            new CustomerEntityTypeConfiguration().Configure(modelBuilder.Entity<Customer>());
            new DriverEntityTypeConfiguration().Configure(modelBuilder.Entity<Driver>());

        }


    }
}
