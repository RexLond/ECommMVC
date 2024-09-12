using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class OrderDetail
    {
        public int ID { get; set; }

        public int OrderID { get; set; }
        public virtual required Order Order { get; set; }

        public required decimal UnitPrice { get; set; }
        public required int Quantity { get; set; }
        public required decimal Discount { get; set; }
        public required decimal VAT { get; set; }
        public required decimal TotalPrice { get; set; }

        public int? ProductID { get; set; }
        public virtual Product? Product { get; set; }
    }
}
