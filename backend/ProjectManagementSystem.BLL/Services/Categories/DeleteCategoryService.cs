using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.BLL.Interfaces.Services.Categories;
using ProductManagementSystem.DAL.Interfaces.Repository;

namespace ProductManagementSystem.BLL.Services.Categories
{
    public class DeleteCategoryService : IDeleteCategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<DeleteCategoryService> _logger;

        public DeleteCategoryService(
            ICategoryRepository repository,
            ILogger<DeleteCategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task ExecuteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete category with ID {CategoryId}", id);

            try
            {
                await _repository.DeleteAsync(id);
                _logger.LogInformation("Category with ID {CategoryId} deleted successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID {CategoryId}", id);
                throw;
            }
        }
    }
}
