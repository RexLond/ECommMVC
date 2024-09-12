using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Abstact
{
    public interface ITicketService: IGenericService<Ticket>
    {
        Task<Ticket> GetByIdWithRefAsync(int id);
        Task<IEnumerable<Ticket>> GetAllWithRefAsync();
    }
}
