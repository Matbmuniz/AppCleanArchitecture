using AppCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCleanArchitecture.Infra.Data.EntitiesConfiguration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(n => n.Name).HasMaxLength(100).IsRequired();
            builder.Property(n => n.Description).HasMaxLength(200).IsRequired();

            builder.Property(n => n.Price).HasPrecision(10,2);

            builder.HasOne(n => n.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
        }
    }
}
