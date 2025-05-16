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
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductImageRepository> _logger;

        public ProductImageRepository(ApplicationDbContext context, ILogger<ProductImageRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ProductImage> GetbyProductIdAsync(int productid)
        {
            _logger.LogInformation("Getting ProductImage by productId {ProductId}", productid);

            var image = await _context.ProductImages
                .Where(pi => pi.ProductId == productid)
                .Select(pi => new ProductImage
                {
                    ImageUrl = pi.ImageUrl,
                })
                .FirstOrDefaultAsync();

            if (image == null)
                _logger.LogWarning("No ProductImage found for productId {ProductId}", productid);
            else
                _logger.LogInformation("Found ProductImage for productId {ProductId}", productid);

            return image;
        }

        public async Task<IEnumerable<ProductImage>> GetAllAsync()
        {
            _logger.LogInformation("Getting all ProductImages");

            var images = await _context.ProductImages
                .Select(pi => new ProductImage
                {
                    ImageUrl = pi.ImageUrl,
                    ProductId = pi.ProductId,
                })
                .ToListAsync();

            _logger.LogInformation("Retrieved {Count} product images", images.Count);
            return images;
        }

        public async Task<ProductImage> CreateAsync(ProductImage req)
        {
            _logger.LogInformation("Creating ProductImage for productId {ProductId}", req.ProductId);

            _context.ProductImages.Add(req);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created ProductImage with ImageUrl {ImageUrl}", req.ImageUrl);
            return req;
        }

        public async Task DeleteByProductIdAsync(int productId)
        {
            _logger.LogInformation("Deleting ProductImage by productId {ProductId}", productId);

            var entity = await _context.ProductImages
                                   .FirstOrDefaultAsync(pi => pi.ProductId == productId);
            if (entity == null)
            {
                _logger.LogWarning("No ProductImage found to delete for productId {ProductId}", productId);
                return;
            }

            var relativePath = entity.ImageUrl.TrimStart('/', '\\');
            var fullPath = Path.Combine("wwwroot", relativePath);

            if (File.Exists(fullPath))
            {
                _logger.LogInformation("Deleting physical file at path {FullPath}", fullPath);
                File.Delete(fullPath);
            }
            else
            {
                _logger.LogWarning("Physical file not found at path {FullPath} for deletion", fullPath);
            }

            _context.ProductImages.Remove(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted ProductImage for productId {ProductId}", productId);
        }

        public async Task UpdateAsync(ProductImage request)
        {
            _logger.LogInformation("Updating ProductImage for productId {ProductId}", request.ProductId);

            var image = await _context.ProductImages
                                  .FirstOrDefaultAsync(pi => pi.ProductId == request.ProductId);

            if (image == null)
            {
                _logger.LogInformation("No existing ProductImage found, adding new one for productId {ProductId}", request.ProductId);

                image = new ProductImage
                {
                    ProductId = request.ProductId,
                    ImageUrl = request.ImageUrl,
                    UploadedDate = DateTime.UtcNow
                };
                _context.ProductImages.Add(image);
            }
            else
            {
                _logger.LogInformation("Existing ProductImage found, updating image URL and upload date for productId {ProductId}", request.ProductId);

                image.ImageUrl = request.ImageUrl;
                image.UploadedDate = DateTime.UtcNow;

                _context.ProductImages.Update(image);
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("ProductImage update/save completed for productId {ProductId}", request.ProductId);
        }
    }
}
