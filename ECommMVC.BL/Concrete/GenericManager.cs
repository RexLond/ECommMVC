using ECommMVC.BL.Abstact;
using ECommMVC.DAL.Abstact;
using ECommMVC.DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericManager(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> CreateAsync(T p)
        {
            return await _repository.CreateAsync(p);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task DeleteBySelectedAsync(List<int> list)
        {
            await _repository.DeleteBySelectedAsync(list);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(T p)
        {
            await _repository.UpdateAsync(p);
        }
    }
}
