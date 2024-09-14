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
        private readonly IPaymentRepository _paymentRepository;

        public OrderManager(IOrderRepository orderRepository, IPaymentRepository paymentRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<Order> GetByIdWithRefAsync(int id)
        {
            return await _orderRepository.GetByIdWithRefAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllWithRefAsync()
        {
            return await _orderRepository.GetAllWithRefAsync();
        }

        // Only BL
        public async Task<Order> CreateOrderWithPaymentAsync(Order order, Payment payment)
        {
            var saveOrder = await _orderRepository.CreateAsync(order);

            payment.Order = saveOrder;
            payment.OrderID = saveOrder.ID;

            await _paymentRepository.CreateAsync(payment);

            return saveOrder;
        }
    }
}
