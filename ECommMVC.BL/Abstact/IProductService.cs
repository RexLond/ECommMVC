using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Abstact
{
    public interface IProductService: IGenericService<Product>
    {
        Task<Product> GetByIdWithRefAsync(int id);
        Task<IEnumerable<Product>> GetAllWithRefAsync();
    }
}
