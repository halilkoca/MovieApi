using App.Core.Response;
using App.Data.Models;
using App.Entity;
using App.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ServiceResponse<TokenModel>> Register([FromBody] RegisterModel model)
        {
            var user = new User
            {
                Email = model.Email,
                Password = model.Password
            };

            return new ServiceResponse<TokenModel>();
        }

        [HttpPost]
        public async Task<ServiceResponse<TokenModel>> Login([FromBody] LoginModel model)
        {
            return new ServiceResponse<TokenModel>();
        }

        [HttpPost]
        public async Task<ServiceResponse<User>> Me(string email)
        {
            return await _userService.GetUser(email);
        }
    }
}
