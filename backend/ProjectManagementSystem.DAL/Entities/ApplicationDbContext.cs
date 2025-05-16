
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.DAL.Entities;
using ProductManagementSystem.DAL.EntityConfigurations;

namespace ProductManagementSystem.DAL.Entities
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<AttributeType> Attributes { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

    
}

