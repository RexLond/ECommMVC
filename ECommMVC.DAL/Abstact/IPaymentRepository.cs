using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Abstact
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Payment> GetByIdWithRefAsync(int id);
        Task<IEnumerable<Payment>> GetAllWithRefAsync();
    }
}
