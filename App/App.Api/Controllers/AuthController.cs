using App.Core.Response;
using App.Data.Models;
using App.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController()
        {

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
    }
}
