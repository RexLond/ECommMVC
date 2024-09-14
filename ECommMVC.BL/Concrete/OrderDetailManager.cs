using ECommMVC.BL.Abstact;
using ECommMVC.DAL.Abstact;
using ECommMVC.DAL.Concrete;
using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Concrete
{
    public class OrderDetailManager : GenericManager<OrderDetail>, IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailManager(IOrderDetailRepository orderDetailRepository) : base(orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<OrderDetail> GetByIdWithRefAsync(int id)
        {
            return await _orderDetailRepository.GetByIdWithRefAsync(id);
        }

        public async Task<IEnumerable<OrderDetail>> GetAllWithRefAsync()
        {
            return await _orderDetailRepository.GetAllWithRefAsync();
        }

        public async Task<List<OrderDetail>> CreateOrderDetailByListAsync(List<OrderDetail> orderDetails)
        {
            return await _orderDetailRepository.CreateOrderDetailByListAsync(orderDetails);
        }
    }
}
