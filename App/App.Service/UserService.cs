using App.Core.Response;
using App.Data;
using App.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace App.Service
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> GetUser(string email);
        Task<ServiceResponse<User>> InsertUser(User user);
        ServiceResponse<User> UpdateUser(User user);
    }

    public class UserService : IUserService
    {
        private readonly EfRepository<User> _userRepo;
        public async Task<ServiceResponse<User>> GetUser(string email)
        {
            return new ServiceResponse<User>(await _userRepo.Table.FirstOrDefaultAsync(x => x.Email == email), true);
        }

        public ServiceResponse<User> UpdateUser(User user)
        {
            var nUser = _userRepo.Get(x => x.Email == user.Email);
            if (nUser == null)
            {
                return new ServiceResponse<User>(false, "User doesnt exist");
            }
            _userRepo.Update(user);
            return new ServiceResponse<User>(user, true);
        }

        public async Task<ServiceResponse<User>> InsertUser(User user)
        {
            var nUser = _userRepo.Get(x => x.Email == user.Email);
            if (nUser != null)
            {
                return new ServiceResponse<User>(false, "User found");
            }
            await _userRepo.Insert(user);
            return new ServiceResponse<User>(user, true);
        }
    }
}
