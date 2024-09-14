using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Abstact
{
    public interface IOrderService: IGenericService<Order>
    {
        Task<Order> GetByIdWithRefAsync(int id);
        Task<IEnumerable<Order>> GetAllWithRefAsync();
        Task<Order> CreateOrderWithPaymentAsync(Order order, Payment payment); // Only BL
    }
}
