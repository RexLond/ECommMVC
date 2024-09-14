using ECommMVC.DAL.Abstact;
using ECommMVC.DAL.Context;
using ECommMVC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ECommContext context) : base(context)
        {

        }

        public async Task<User> GetUserByMailAndPassword (string email, string password)
        {
            try
            {
                return await _context.Set<User>()
                    .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Get User {email} failed data failed. Error: {ex.Message}", ex);
            }
        }

        public async Task<bool> CheckMail(string email)
        {
            try
            {
                var user = await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Get User {email} failed data failed. Error: {ex.Message}", ex);
            }
        }
    }
}
