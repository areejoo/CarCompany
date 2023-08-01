using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using web.core.Data;
using web.core.Models;


namespace web.core.Data.Configuration
{


public class DriverEntityTypeConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
         builder
            .Property(c => c.Name).IsRequired();
         builder   
            .Property(c => c.Phone).IsRequired();

    

    }
}
}