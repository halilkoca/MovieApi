using App.Core.Extensions;
using App.Core.Utilities.Interceptors;
using App.Core.Utilities.IoC;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace App.Core.Aspects.Authorization
{
    public class AuthorizationAspect : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private string _roles;

        public AuthorizationAspect(string roles)
        {
            _roles = roles;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override void OnBefore(IInvocation invocation)
        {
            var currentUserClaims = _httpContextAccessor.HttpContext.User?.Claims.ToList();
            var currentUserRoles = currentUserClaims?.GetRoles();

            var roles = _roles.Split(',');
            foreach (var userRole in currentUserRoles)
            {
                if (roles.Contains(userRole))
                {
                    return;
                }
            }

            throw new Exception("AuthorizeDenied");
        }
    }
}
