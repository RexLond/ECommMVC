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
    public class TicketManager : GenericManager<Ticket>, ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketManager(ITicketRepository ticketRepository) : base(ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> GetByIdWithRefAsync(int id)
        {
            return await _ticketRepository.GetByIdWithRefAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetAllWithRefAsync()
        {
            return await _ticketRepository.GetAllWithRefAsync();
        }
    }
}
