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
    public class ListCategoriesService : IListCategoriesService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListCategoriesService> _logger;

        public ListCategoriesService(
            ICategoryRepository repository,
            IMapper mapper,
            ILogger<ListCategoriesService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<GetCategoryResponse>> ExecuteAsync()
        {
            _logger.LogInformation("Listing all categories");
            try
            {
                var entities = await _repository.ListAsync();
                var dtos = _mapper.Map<IEnumerable<GetCategoryResponse>>(entities);
                _logger.LogInformation("Retrieved {Count} categories", dtos.Count());
                return dtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing categories");
                throw;
            }
        }
    }
}
