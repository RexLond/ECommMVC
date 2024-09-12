using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class Payment
    {
        public int ID { get; set; }
        public required string PaymentMethod { get; set; }
        public required decimal Freight { get; set; }
        public string? InvoiceNo { get; set; }

        public int? OrderID { get; set; }
        public virtual Order? Order { get; set; }

        public int? UserID { get; set; }
        public virtual User? User { get; set; }
    }
}
