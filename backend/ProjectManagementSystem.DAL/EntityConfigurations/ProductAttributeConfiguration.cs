using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.DAL.Entities;

namespace ProductManagementSystem.DAL.EntityConfigurations
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> entity)
        {
            entity.ToTable("ProductAttributes");
            entity.HasKey(pa => new { pa.ProductId, pa.AttributeId });
            entity.Property(pa => pa.Value).HasMaxLength(500);
            entity.Property(pa => pa.AssignedDate).HasDefaultValueSql("GETDATE()");
            entity.HasOne(pa => pa.Product)
                  .WithMany(p => p.ProductAttributes)
                  .HasForeignKey(pa => pa.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(pa => pa.AttributeType)
                  .WithMany(a => a.ProductAttributes)
                  .HasForeignKey(pa => pa.AttributeId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
