using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.BLL.DTOs.Category;

namespace ProductManagementSystem.BLL.Interfaces.Services.Categories
{
    public interface IUpdateCategoryService
    {
        Task<GetCategoryResponse?> ExecuteAsync(UpdateCategoryRequest request);
    }
}
