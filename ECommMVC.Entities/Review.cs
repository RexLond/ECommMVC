using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class Review
    {
        public int ID { get; set; }
        public string? Text { get; set; }
        public required byte Rating { get; set; }
        public bool IsApproved { get; set; } = false;

        public int ProductID { get; set; }
        public virtual Product? Product { get; set; }

        public int UserID { get; set; }
        public virtual User? User { get; set; }
    }
}
