using ECommMVC.DAL.Abstact;
using ECommMVC.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ECommContext _context;

        public GenericRepository(ECommContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T p)
        {
            try
            {
                await _context.Set<T>().AddAsync(p);
                await _context.SaveChangesAsync();
                return p;
            }
            catch (Exception ex)
            {
                throw new Exception($"Create failed. Error: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                T entity = await GetByIdAsync(id);
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Delete failed. Error: {ex.Message}", ex);
            }
        }

        public async Task DeleteBySelectedAsync(List<int> list)
        {
            try
            {
                foreach (int id in list)
                {
                    T entity = await GetByIdAsync(id);
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex) 
            {
                throw new Exception($"Delete failed. Error: {ex.Message}", ex);
            }
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception($"Get all data failed. Error: {ex.Message}", ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            } catch (Exception ex)
            {
                throw new Exception($"Get id={id} data failed. Error: {ex.Message}", ex);
            }
            
        }

        public async Task UpdateAsync(T p)
        {
            try
            {
                _context.Set<T>().Update(p);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Update failed. Error: {ex.Message}", ex);
            }
        }
    }
}
