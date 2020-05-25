using App.Core.Entities;
using App.Core.Response;
using App.Core.Utilities.Jwt;
using App.Core.Utilities.Security;
using App.Data.Models;
using App.Entity;
using App.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        public AuthController(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        /// <summary>
        /// Register definition
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<ServiceResponse<TokenModel>> Register([FromBody] RegisterModel model)
        {
            var user = new User
            {
                Email = model.Email,
                PasswordHash = new byte[0]
            };

            return new ServiceResponse<TokenModel>();
        }

        [HttpPost("Login")]
        public async Task<ServiceResponse<TokenModel>> Login(LoginModel model)
        {
            var user = await _userService.GetUser(model.Email);
            if (user == null)
                return new ServiceResponse<TokenModel>(false, "UserDoesNotExist");

            if (!HashingHelper.VerifyPasswordHash(model.Password, user.Data.PasswordHash, user.Data.PasswordSalt))
                return new ServiceResponse<TokenModel>(false, "EmailOrPasswordError");

            var accessToken = _tokenHelper.CreateToken(user.Data);

            return new ServiceResponse<TokenModel>(accessToken, true);
        }

        [HttpGet("Me")]
        public async Task<ServiceResponse<User>> Me(string email)
        {
            return await _userService.GetUser(email);
        }
    }
}
