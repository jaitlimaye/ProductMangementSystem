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
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> entity)
        {
            entity.ToTable("ProductImages");
            entity.HasKey(pi => pi.ImageId);
            entity.Property(pi => pi.ImageUrl).IsRequired().HasDefaultValue("/uploads/default.jpg"); 
            entity.Property(pi => pi.UploadedDate).HasDefaultValueSql("GETDATE()");
            entity.HasIndex(pi => pi.ProductId)
              .IsUnique();

            entity.HasOne(pi => pi.Product)
                  .WithOne(p => p.ProductImage)       // ← one-to-one
                  .HasForeignKey<ProductImage>(pi => pi.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
