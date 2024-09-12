using ECommMVC.BL.Abstact;
using ECommMVC.DAL.Abstact;
using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Concrete
{
    public class OrderManager : GenericManager<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetByIdWithRefAsync(int id)
        {
            return await _orderRepository.GetByIdWithRefAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllWithRefAsync()
        {
            return await _orderRepository.GetAllWithRefAsync();
        }
    }
}
