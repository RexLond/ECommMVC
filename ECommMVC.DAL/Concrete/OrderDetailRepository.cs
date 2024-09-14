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
    public class OrderDetailRepository: GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ECommContext context) : base(context)
        {
            
        }

        public async Task<OrderDetail> GetByIdWithRefAsync(int id)
        {
            try
            {
                return await _context.Set<OrderDetail>()
                    .Include(c => c.Order)
                    .Include(c => c.Product)
                    .FirstOrDefaultAsync(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Get id={id} data failed. Error: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetAllWithRefAsync()
        {
            try
            {
                return await _context.Set<OrderDetail>()
                    .Include(c => c.Order)
                    .Include(c => c.Product)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Get all data failed. Error: {ex.Message}", ex);
            }
        }

        public async Task<List<OrderDetail>> CreateOrderDetailByListAsync(List<OrderDetail> orderDetails)
        {
            try
            {
                foreach (var orderDetail in orderDetails)
                {
                    await _context.Set<OrderDetail>().AddAsync(orderDetail);
                    await _context.SaveChangesAsync();
                }
                return orderDetails;
            }
            catch (Exception ex)
            {
                throw new Exception($"Data list crate failed. Error: {ex.Message}", ex);
            }
        }
    }
}
