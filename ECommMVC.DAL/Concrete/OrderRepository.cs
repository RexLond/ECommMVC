using ECommMVC.DAL.Abstact;
using ECommMVC.DAL.Context;
using ECommMVC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Concrete
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ECommContext context) : base(context)
        {
            
        }

        public async Task<Order> GetByIdWithRefAsync(int id)
        {
            try
            {
                return await _context.Set<Order>()
                    .Include(c => c.Shipper)
                    .Include(c => c.User)
                    .Include(c => c.Payment)
                    .FirstOrDefaultAsync(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Get id={id} data failed. Error: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetAllWithRefAsync()
        {
            try
            {
                return await _context.Set<Order>()
                    .Include(c => c.Shipper)
                    .Include(c => c.User)
                    .Include(c => c.Payment)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Get all data failed. Error: {ex.Message}", ex);
            }
        }
    }
}
