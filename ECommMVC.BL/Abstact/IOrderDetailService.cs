using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Abstact
{
    public interface IOrderDetailService: IGenericService<OrderDetail>
    {
        Task<OrderDetail> GetByIdWithRefAsync(int id);
        Task<IEnumerable<OrderDetail>> GetAllWithRefAsync();
        Task<List<OrderDetail>> CreateOrderDetailByListAsync(List<OrderDetail> orderDetails);
    }
}
