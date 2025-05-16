using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.DAL.Interfaces.Repository;
using Azure.Core;
using ProductManagementSystem.BLL.Interfaces.Services.Products;
using Microsoft.Extensions.Logging;

namespace ProductManagementSystem.BLL.Services.Products
{
    public class DeleteProductService : IDeleteProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductImageRepository _imagerepository;
        private readonly ILogger<DeleteProductService> _logger;

        public DeleteProductService(
            IProductRepository repository,
            IProductImageRepository imageRepository,
            ILogger<DeleteProductService> logger)
        {
            _repository = repository;
            _imagerepository = imageRepository;
            _logger = logger;
        }

        public async Task ExecuteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting product images for product ID: {ProductId}", id);
                await _imagerepository.DeleteByProductIdAsync(id);
                _logger.LogInformation("Deleted product images for product ID: {ProductId}", id);

                _logger.LogInformation("Deleting product with ID: {ProductId}", id);
                await _repository.DeleteAsync(id);
                _logger.LogInformation("Deleted product with ID: {ProductId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product with ID: {ProductId}", id);
                throw;
            }
        }
    }
}
