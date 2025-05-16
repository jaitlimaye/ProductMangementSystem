using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.DAL.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
        public virtual ProductImage? ProductImage { get; set; }
    }
}
