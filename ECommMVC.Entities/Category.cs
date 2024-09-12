using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
