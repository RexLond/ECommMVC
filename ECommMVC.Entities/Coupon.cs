using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class Coupon
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Code { get; set; }
        public required decimal Discount { get; set; }
        public required DateTime ActivatedAt {  get; set; }
        public required DateTime PassivedAt { get; set; }
    }
}
