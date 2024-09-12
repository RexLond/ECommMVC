using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Abstact
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();           // Get All Data => List * Async
        Task<T> GetByIdAsync(int id);                 // Get One Data by ID => ClassType * Async
        Task<T> CreateAsync(T p);                     // Create One Data => ClassType * Async
        Task UpdateAsync(T p);                        // Update One Data
        Task DeleteAsync(int id);                     // Delete One Data by ID
        Task DeleteBySelectedAsync(List<int> list);   // Delete Selected Data by ID
    }
}
