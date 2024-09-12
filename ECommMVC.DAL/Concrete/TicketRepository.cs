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
    public class TicketRepository: GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ECommContext context) : base(context)
        {
            
        }

        public async Task<Ticket> GetByIdWithRefAsync(int id)
        {
            try
            {
                return await _context.Set<Ticket>()
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Get id={id} data failed. Error: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllWithRefAsync()
        {
            try
            {
                return await _context.Set<Ticket>()
                    .Include(c => c.User)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Get all data failed. Error: {ex.Message}", ex);
            }
        }
    }
}
