using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.DAL.Entities
{
    public class ProductAttribute
    {
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public string? Value { get; set; }
        public DateTime AssignedDate { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual AttributeType AttributeType { get; set; } = null!;
    }
}
