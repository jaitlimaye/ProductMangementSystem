using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagementSystem.DAL.Entities;

namespace ProductManagementSystem.DAL.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Products");
            entity.HasKey(p => p.ProductId);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Price).HasColumnType("DECIMAL(18,2)").HasDefaultValue(0.00m);
            entity.Property(p => p.CreatedDate).HasDefaultValueSql("GETDATE()");
            entity.HasOne(p => p.ProductImage)
              .WithOne(pi => pi.Product)
              .HasForeignKey<ProductImage>(pi => pi.ProductId);
        }
    }
}
