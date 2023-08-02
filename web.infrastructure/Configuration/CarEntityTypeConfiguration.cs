
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using web.core.Entities;


namespace web.infrastructure.Configuration
{


public class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder
            .HasIndex(c => c.Number)
            .IsUnique();
            builder.HasOne(c => c.Customer).WithMany(c => c.Cars).HasForeignKey(c => c.Customer.Id);  

    }
}
}