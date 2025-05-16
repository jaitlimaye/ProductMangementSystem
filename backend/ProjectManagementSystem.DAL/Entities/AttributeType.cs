using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.DAL.Entities
{
    public class AttributeType
    {
        public int AttributeId { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
    }
}
