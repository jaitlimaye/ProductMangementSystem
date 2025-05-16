using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.DAL.Interfaces.Repository;
using ProductManagementSystem.DAL.Entities;
using AutoMapper;
using ProductManagementSystem.BLL.Interfaces.Services.Products;
using ProductManagementSystem.BLL.DTOs.Product;
using Microsoft.Extensions.Logging;

namespace ProductManagementSystem.BLL.Services.Products
{
    public class UpdateProductService : IUpdateProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductService> _logger;

        public UpdateProductService(
            IProductRepository repository,
            IMapper mapper,
            ILogger<UpdateProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DetailProductResponse> ExecuteAsync(UpdateProductRequest request)
        {
            _logger.LogInformation("Starting update for product with ID {ProductId}", request.ProductId);

            var existing = await _repository.GetAsync(request.ProductId);
            if (existing == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for update", request.ProductId);
                throw new KeyNotFoundException($"Product {request.ProductId} not found.");
            }

            _mapper.Map(request, existing);
            _logger.LogInformation("Mapped update data to product entity for ID {ProductId}", request.ProductId);

            if (request.Image != null)
            {
                _logger.LogInformation("New image provided for product ID {ProductId}. Saving image file.", request.ProductId);

                var uploadsFolder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueName = Guid.NewGuid() + Path.GetExtension(request.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueName);
                await using var stream = new FileStream(filePath, FileMode.Create);
                await request.Image.CopyToAsync(stream);

                existing.ProductImage = new ProductImage
                {
                    ImageUrl = $"/uploads/{uniqueName}"
                };

                _logger.LogInformation("Image uploaded and attached to product ID {ProductId}", request.ProductId);
            }
            else if (request.Image == null && request.CurrentImageUrl == null)
            {
                _logger.LogInformation("No image provided and no current image URL. Removing image from product ID {ProductId}", request.ProductId);
                existing.ProductImage = null;
            }
            else
            {
                _logger.LogInformation("Keeping existing image for product ID {ProductId}", request.ProductId);
            }

            var updated = await _repository.UpdateAsync(existing);
            _logger.LogInformation("Successfully updated product with ID {ProductId}", request.ProductId);

            return _mapper.Map<DetailProductResponse>(updated);
        }
    }
}
