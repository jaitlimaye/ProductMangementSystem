using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.BLL.DTOs.Product;
using ProductManagementSystem.BLL.DTOs.ProductImage;
using ProductManagementSystem.BLL.Interfaces.Services.Products;
using ProductManagementSystem.DAL.Entities;
using ProductManagementSystem.DAL.Interfaces.Repository;

namespace ProductManagementSystem.BLL.Services.Products
{
    public class CreateProductService : ICreateProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductImageRepository _imagerepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductService> _logger;

        public CreateProductService(
            IProductRepository repository,
            IProductImageRepository imageRepository,
            IMapper mapper,
            ILogger<CreateProductService> logger)
        {
            _repository = repository;
            _imagerepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DetailProductResponse> ExecuteAsync(CreateProductRequest request)
        {
            try
            {
                _logger.LogInformation("Creating product with name: {ProductName}", request.Name);

                var productEntity = _mapper.Map<Product>(request);
                var savedEntity = await _repository.CreateAsync(productEntity);
                _logger.LogInformation("Product created with ID: {ProductId}", savedEntity.ProductId);

                if (request.Image != null)
                {
                    var uploadsFolder = Path.Combine("wwwroot", "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueName = Guid.NewGuid() + Path.GetExtension(request.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueName);

                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await request.Image.CopyToAsync(stream);
                    _logger.LogInformation("Product image saved to: {FilePath}", filePath);

                    var imgEntity = _mapper.Map<ProductImage>(new CreateProductImageRequest
                    {
                        ProductId = savedEntity.ProductId,
                        ImageUrl = $"/uploads/{uniqueName}"
                    });

                    await _imagerepository.CreateAsync(imgEntity);
                    _logger.LogInformation("Product image record created for product ID: {ProductId}", savedEntity.ProductId);
                }

                return _mapper.Map<DetailProductResponse>(savedEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product: {ProductName}", request.Name);
                throw;
            }
        }
    }
}
