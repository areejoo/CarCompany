
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using web.core.Entities;


namespace web.infrastructure.Configuration
{


public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
         builder
            .Property(c => c.Name).IsRequired();
            
       
        builder
            .Property(c => c.Phone).IsRequired();
        
        builder
            .Property(c => c.Email).IsRequired();

    

       

    }
}
}