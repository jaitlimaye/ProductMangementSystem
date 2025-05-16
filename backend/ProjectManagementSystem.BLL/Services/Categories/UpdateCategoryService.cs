using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.BLL.DTOs.Category;
using ProductManagementSystem.BLL.Interfaces.Services.Categories;
using ProductManagementSystem.DAL.Entities;
using ProductManagementSystem.DAL.Interfaces.Repository;

namespace ProductManagementSystem.BLL.Services.Categories
{
    public class UpdateCategoryService : IUpdateCategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCategoryService> _logger;

        public UpdateCategoryService(
            ICategoryRepository repository,
            IMapper mapper,
            ILogger<UpdateCategoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetCategoryResponse?> ExecuteAsync(UpdateCategoryRequest request)
        {
            _logger.LogInformation("Updating category with ID {CategoryId}", request.CategoryId);

            try
            {
                var toUpdate = _mapper.Map<Category>(request);
                var updated = await _repository.UpdateAsync(toUpdate);

                if (updated == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found for update", request.CategoryId);
                    return null;
                }

                _logger.LogInformation("Successfully updated category with ID {CategoryId}", request.CategoryId);
                return _mapper.Map<GetCategoryResponse>(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category with ID {CategoryId}", request.CategoryId);
                throw;
            }
        }
    }
}
