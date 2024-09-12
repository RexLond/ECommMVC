using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public required decimal Freight { get; set; }
        public required DateTime OrderDate { get; set; }
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public required string ShipAddress { get; set; }
        public required string ShipCity { get; set; }
        public required string ShipRegion { get; set; }
        public required string ShipPostalCode { get; set; }
        public required string ShipCountry { get; set; }

        public int? ShipperID { get; set; }
        public virtual Shipper? Shipper { get; set; }

        public int? UserID { get; set; }
        public virtual User? User { get; set; }

        public int? PaymentID { get; set; }
        public virtual Payment? Payment { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
