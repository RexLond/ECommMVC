using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class Ticket
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public required string Message { get; set; }

        public int? UserID { get; set; } 
        public virtual User? User { get; set; }
    }
}
