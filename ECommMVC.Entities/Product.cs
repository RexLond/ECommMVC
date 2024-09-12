using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class Product
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal UnitPrice { get; set; }
        public required int Quantity { get; set; }
        public required decimal VAT { get; set; }
        public required decimal Discount { get; set; }
        public required decimal TotalPrice { get; set; }
        public string? Image { get; set; }

        public int? CategoryID { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
