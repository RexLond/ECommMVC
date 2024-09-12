using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Abstact
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetByIdWithRefAsync(int id);
        Task<IEnumerable<Order>> GetAllWithRefAsync();
    }
}
