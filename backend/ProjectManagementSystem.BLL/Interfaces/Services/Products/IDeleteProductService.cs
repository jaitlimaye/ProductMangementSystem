using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.BLL.Interfaces.Services.Products
{
    public interface IDeleteProductService
    {
        Task ExecuteAsync(int id);
    }
}
