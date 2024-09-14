using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Abstact
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByMailAndPassword(string email, string password);
        Task<bool> CheckMail(string email);
    }
}
