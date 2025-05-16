using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.DAL.Entities;
namespace ProductManagementSystem.DAL.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(int id);
        Task<IEnumerable<Product>> ListAsync();
        Task<Product> CreateAsync(Product request);
        Task<Product> UpdateAsync(Product request);
        Task<Product> PatchAsync(int id, Product request);
        Task DeleteAsync(int id);
    }
}
