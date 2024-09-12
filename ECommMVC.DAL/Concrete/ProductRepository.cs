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
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ECommContext context) : base(context)
        {
            
        }

        public async Task<Product> GetByIdWithRefAsync(int id)
        {
            try
            {
                return await _context.Set<Product>()
                    .Include(c => c.Category)
                    .FirstOrDefaultAsync(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Get id={id} data failed. Error: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Product>> GetAllWithRefAsync()
        {
            try
            {
                return await _context.Set<Product>()
                    .Include(c => c.Category)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Get all data failed. Error: {ex.Message}", ex);
            }
        }
    }
}
