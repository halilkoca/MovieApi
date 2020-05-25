using App.Core.DbTrackers;
using App.Core.Entities;
using App.Core.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<UserClaim> _userClaimRepo;
        public UserService(
            IRepository<User> userRepo, IRepository<UserClaim> userClaimRepo
            )
        {
            _userRepo = userRepo;
            _userClaimRepo = userClaimRepo;
        }
        public async Task<ServiceResponse<User>> GetUser(string email)
        {
            return new ServiceResponse<User>(await _userRepo.Table.Include(a => a.UserClaims).FirstOrDefaultAsync(x => x.Email == email), true);
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
            var nUser = _userRepo.Table.FirstOrDefault(x => x.Email == user.Email);
            if (nUser != null) return new ServiceResponse<User>(false, "UserExist");
            var newId = _userRepo.Table.Select(x => x.Id).Max() + 1;
            user.Id = newId;
            await _userRepo.Insert(user);

            // add default claim
            var newIdd = _userClaimRepo.Table.Select(x => x.Id).Max() + 1;
            await _userClaimRepo.Insert(new UserClaim { Id = newIdd, OClaimId = 2, UserId = newId });

            user = await _userRepo.Table.Include(a => a.UserClaims).ThenInclude(o => o.OClaim).FirstOrDefaultAsync(a => a.Id == newId);

            return new ServiceResponse<User>(user, true);
        }


    }
}
