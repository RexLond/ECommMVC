using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Abstact
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<Ticket> GetByIdWithRefAsync(int id);
        Task<IEnumerable<Ticket>> GetAllWithRefAsync();
    }
}
