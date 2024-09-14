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
    public class UserManager : GenericManager<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByMailAndPassword(string email, string password)
        {
            return await _userRepository.GetUserByMailAndPassword(email, password);
        }

        public async Task<bool> CheckMail(string email)
        {
            return await _userRepository.CheckMail(email);
        }
    }
}
