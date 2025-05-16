using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.DAL.Entities;
using ProductManagementSystem.DAL.Interfaces.Repository;

namespace ProductManagementSystem.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Product> GetAsync(int id)
        {
            _logger.LogInformation("Fetching product with ID {ProductId}", id);
            var entity = await _context.Products.FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", id);
                return null!;
            }

            _logger.LogInformation("Product with ID {ProductId} found", id);
            return new Product
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            _logger.LogInformation("Fetching list of all products");
            var products = await _context.Products
                .Select(p => new Product
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();
            _logger.LogInformation("Fetched {Count} products", products.Count);
            return products;
        }

        public async Task<Product> CreateAsync(Product request)
        {
            _logger.LogInformation("Creating new product with Name: {Name}", request.Name);
            var entity = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId
            };
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created product with ID {ProductId}", entity.ProductId);
            return new Product
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task<Product> UpdateAsync(Product request)
        {
            _logger.LogInformation("Updating product with ID {ProductId}", request.ProductId);
            var entity = await _context.Products.FindAsync(request.ProductId);
            if (entity == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for update", request.ProductId);
                return null!;
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Price = request.Price;
            entity.CategoryId = request.CategoryId;

            _context.Products.Update(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated product with ID {ProductId}", entity.ProductId);
            return new Product
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task<Product> PatchAsync(int id, Product request)
        {
            _logger.LogInformation("Patching product with ID {ProductId}", id);
            var entity = await _context.Products.FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for patching", id);
                return null!;
            }

            if (request.Name is not null)
                entity.Name = request.Name;
            if (request.Description is not null)
                entity.Description = request.Description;
            entity.Price = request.Price;
            entity.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Patched product with ID {ProductId}", id);

            return new Product
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting product with ID {ProductId}", id);
            var entity = await _context.Products.FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for deletion", id);
                return;
            }

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted product with ID {ProductId}", id);
        }
    }
}
