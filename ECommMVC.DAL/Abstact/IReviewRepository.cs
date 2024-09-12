using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Abstact
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<Review> GetByIdWithRefAsync(int id);
        Task<IEnumerable<Review>> GetAllWithRefAsync();
    }
}
