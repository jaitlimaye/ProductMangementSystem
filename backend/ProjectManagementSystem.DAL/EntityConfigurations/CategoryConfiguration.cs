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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Categories");
            entity.HasKey(c => c.CategoryId);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(c => c.Name).IsUnique();
            entity.Property(c => c.CreatedDate).HasDefaultValueSql("GETDATE()");
        }
    }
}
