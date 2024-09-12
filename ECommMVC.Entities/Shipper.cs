using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class Shipper
    {
        public int ID { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
