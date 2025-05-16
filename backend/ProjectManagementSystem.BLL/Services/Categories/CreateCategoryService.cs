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
    public class CreateCategoryService : ICreateCategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryService> _logger;

        public CreateCategoryService(
            ICategoryRepository repository,
            IMapper mapper,
            ILogger<CreateCategoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetCategoryResponse> ExecuteAsync(CreateCategoryRequest request)
        {
            _logger.LogInformation("Attempting to create category with Name {Name}", request.Name);

            try
            {
                // Map DTO to entity
                var entity = _mapper.Map<Category>(request);

                // Persist
                var saved = await _repository.CreateAsync(entity);

                _logger.LogInformation("Category created successfully with ID {CategoryId}", saved.CategoryId);

                // Map back to response DTO
                return _mapper.Map<GetCategoryResponse>(saved);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category with Name {Name}", request.Name);
                throw;
            }
        }
    }
}
