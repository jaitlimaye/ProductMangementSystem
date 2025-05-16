using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.DAL.Interfaces.Repository;
using AutoMapper;
using ProductManagementSystem.DAL.Entities;
using ProductManagementSystem.BLL.Interfaces.Services.Products;
using ProductManagementSystem.BLL.DTOs.Product;
using Microsoft.Extensions.Logging;

namespace ProductManagementSystem.BLL.Services.Products
{
    public class PatchProductService : IPatchProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatchProductService> _logger;

        public PatchProductService(IProductRepository repository, IMapper mapper, ILogger<PatchProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DetailProductResponse?> ExecuteAsync(int id, PatchProductRequest request)
        {
            _logger.LogInformation("Attempting to patch product with ID {ProductId}", id);

            var toPatch = _mapper.Map<Product>(request);
            var patched = await _repository.PatchAsync(id, toPatch);

            if (patched == null)
            {
                _logger.LogWarning("Patch failed. Product with ID {ProductId} not found", id);
                return null;
            }

            _logger.LogInformation("Product with ID {ProductId} successfully patched", id);
            return _mapper.Map<DetailProductResponse>(patched);
        }
    }
}
