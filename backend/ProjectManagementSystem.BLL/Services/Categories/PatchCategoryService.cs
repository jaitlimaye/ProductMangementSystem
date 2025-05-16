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
    public class PatchCategoryService : IPatchCategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatchCategoryService> _logger;

        public PatchCategoryService(
            ICategoryRepository repository,
            IMapper mapper,
            ILogger<PatchCategoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetCategoryResponse?> ExecuteAsync(int id, PatchCategoryRequest request)
        {
            _logger.LogInformation("Patching category with ID {CategoryId}", id);

            try
            {
                var toPatch = _mapper.Map<Category>(request);
                var patched = await _repository.PatchAsync(id, toPatch);

                if (patched == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found for patching", id);
                    return null;
                }

                _logger.LogInformation("Successfully patched category with ID {CategoryId}", id);
                return _mapper.Map<GetCategoryResponse>(patched);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while patching category with ID {CategoryId}", id);
                throw;
            }
        }
    }
}
