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
    public class AttributeTypeConfiguration : IEntityTypeConfiguration<AttributeType>
    {
        public void Configure(EntityTypeBuilder<AttributeType> entity)
        {
            entity.ToTable("Attributes");
            entity.HasKey(a => a.AttributeId);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(a => a.Name).IsUnique();
        }
    }
}
