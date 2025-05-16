using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.DAL.Entities;

namespace ProductManagementSystem.DAL.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<Category?> GetAsync(int id);
        Task<Category> CreateAsync(Category request);
        Task<Category?> UpdateAsync(Category request);
        Task<Category?> PatchAsync(int id, Category request);
        Task DeleteAsync(int id);
    }
}
