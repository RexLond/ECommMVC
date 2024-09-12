using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Abstact
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetByIdWithRefAsync(int id);
        Task<IEnumerable<Product>> GetAllWithRefAsync();
    }
}
