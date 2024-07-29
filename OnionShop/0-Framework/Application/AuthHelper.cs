using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string CurrentAccountRole()
        {
            if (IsAuthenticated())
            {
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            }

            return null;
        }

        public bool IsAuthenticated()
        {
           var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            return claims.Count > 0;
        }

        public void SignIn(AuthViewModel auth)
        {
            var clamis = new List<Claim>
            {
                new Claim("AccountId",auth.Id.ToString()),
                new Claim(ClaimTypes.Name , auth.FullName),
                new Claim(ClaimTypes.Role , auth.RoleId.ToString()),
                new Claim("Username" , auth.UserName)
            };

            var claimIdentity = new ClaimsIdentity(clamis,CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties { 
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimIdentity),authProperties);

        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
