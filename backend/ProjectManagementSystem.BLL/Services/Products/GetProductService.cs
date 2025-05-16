using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.DAL.Interfaces.Repository;
using AutoMapper;
using ProductManagementSystem.BLL.Interfaces.Services.Products;
using ProductManagementSystem.BLL.DTOs.Product;
using Microsoft.Extensions.Logging;

namespace ProductManagementSystem.BLL.Services.Products
{
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductService> _logger;

        public GetProductService(IProductRepository repository, IMapper mapper, ILogger<GetProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DetailProductResponse?> ExecuteAsync(int id)
        {
            _logger.LogInformation("Fetching product with ID {ProductId}", id);

            var entity = await _repository.GetAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", id);
                return null;
            }

            _logger.LogInformation("Product with ID {ProductId} successfully retrieved", id);
            return _mapper.Map<DetailProductResponse>(entity);
        }
    }

}
