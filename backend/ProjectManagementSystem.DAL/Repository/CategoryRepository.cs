using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.DAL.Entities;
using ProductManagementSystem.DAL.Interfaces.Repository;

namespace ProductManagementSystem.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(ApplicationDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            _logger.LogInformation("Listing all categories");

            var categories = await _context.Categories
                .Select(c => new Category
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();

            _logger.LogInformation("Retrieved {Count} categories", categories.Count);
            return categories;
        }

        public async Task<Category?> GetAsync(int id)
        {
            _logger.LogInformation("Getting category with id {CategoryId}", id);

            var c = await _context.Categories.FindAsync(id);
            if (c == null)
            {
                _logger.LogWarning("Category with id {CategoryId} not found", id);
                return null;
            }

            _logger.LogInformation("Category with id {CategoryId} found", id);
            return new Category
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedDate = c.CreatedDate
            };
        }

        public async Task<Category> CreateAsync(Category request)
        {
            _logger.LogInformation("Creating a new category with name {CategoryName}", request.Name);

            _context.Categories.Add(request);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created category with id {CategoryId}", request.CategoryId);
            return request;
        }

        public async Task<Category?> UpdateAsync(Category request)
        {
            _logger.LogInformation("Updating category with id {CategoryId}", request.CategoryId);

            var entity = await _context.Categories.FindAsync(request.CategoryId);
            if (entity == null)
            {
                _logger.LogWarning("Category with id {CategoryId} not found for update", request.CategoryId);
                return null;
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated category with id {CategoryId}", entity.CategoryId);

            return new Category
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Description = entity.Description,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task<Category> PatchAsync(int id, Category request)
        {
            _logger.LogInformation("Patching category with id {CategoryId}", id);

            var entity = await _context.Categories.FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Category with id {CategoryId} not found for patch", id);
                return null;
            }

            if (request.Name is not null)
            {
                entity.Name = request.Name;
                _logger.LogInformation("Patched 'Name' field for category id {CategoryId}", id);
            }
            if (request.Description is not null)
            {
                entity.Description = request.Description;
                _logger.LogInformation("Patched 'Description' field for category id {CategoryId}", id);
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Patched category with id {CategoryId}", id);

            return new Category
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Description = entity.Description,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting category with id {CategoryId}", id);

            var entity = await _context.Categories.FindAsync(id);
            if (entity != null)
            {
                _context.Categories.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Deleted category with id {CategoryId}", id);
            }
            else
            {
                _logger.LogWarning("Category with id {CategoryId} not found for deletion", id);
            }
        }
    }
}
