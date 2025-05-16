using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.BLL.DTOs.Product;
using ProductManagementSystem.BLL.Interfaces.Services.Products;
using ProductManagementSystem.DAL.Interfaces.Repository;
using ProductManagementSystem.DAL.Repository;

namespace ProductManagementSystem.BLL.Services.Products
{
    public class GetAllProductDetailsService : IGetAllProductDetailsService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productimagerepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductDetailsService> _logger;

        public GetAllProductDetailsService(
            IProductRepository productRepository,
            IProductImageRepository productimagerepository,
            IMapper mapper,
            ILogger<GetAllProductDetailsService> logger)
        {
            _productRepository = productRepository;
            _productimagerepository = productimagerepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<GetAllProductsResponse>> ExecuteAsync()
        {
            _logger.LogInformation("Fetching all products and associated images.");

            var products = await _productRepository.ListAsync();
            var productImages = await _productimagerepository.GetAllAsync();

            _logger.LogInformation("Fetched {ProductCount} products and {ImageCount} images.",
                products.Count(), productImages.Count());

            var productDetails = products.Select(product => new GetAllProductsResponse
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = productImages.FirstOrDefault(pi => pi.ProductId == product.ProductId)?.ImageUrl ?? "/uploads/default.jpg"
            });

            _logger.LogInformation("Returning product details for {ProductCount} products.", productDetails.Count());

            return productDetails;
        }
    }
}
