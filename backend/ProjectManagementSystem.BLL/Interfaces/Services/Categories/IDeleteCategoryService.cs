using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.BLL.Interfaces.Services.Categories
{
    public interface IDeleteCategoryService
    {
        Task ExecuteAsync(int id);
    }
}
