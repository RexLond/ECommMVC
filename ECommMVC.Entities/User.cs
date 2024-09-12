using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class User
    {
        public int ID { get; set; }
        public required string Identity { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public required string Password { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string Region { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }
        public required string Phone { get; set; }
        public bool IsPhoneConfirmed { get; set; } = false;
        public string? Photo { get; set; }
        public bool IsAdmin { get; set; } = false;

        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
}
