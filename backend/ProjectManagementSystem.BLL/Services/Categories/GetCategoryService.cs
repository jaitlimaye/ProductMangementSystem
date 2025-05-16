using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.BLL.DTOs.Category;
using ProductManagementSystem.BLL.Interfaces.Services.Categories;
using ProductManagementSystem.DAL.Interfaces.Repository;

namespace ProductManagementSystem.BLL.Services.Categories
{
    public class GetCategoryService : IGetCategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoryService> _logger;

        public GetCategoryService(
            ICategoryRepository repository,
            IMapper mapper,
            ILogger<GetCategoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetCategoryResponse?> ExecuteAsync(int id)
        {
            _logger.LogInformation("Retrieving category with ID {CategoryId}", id);

            try
            {
                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found", id);
                    return null;
                }

                var dto = _mapper.Map<GetCategoryResponse>(entity);
                _logger.LogInformation("Category with ID {CategoryId} retrieved successfully", id);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category with ID {CategoryId}", id);
                throw;
            }
        }
    }
}
