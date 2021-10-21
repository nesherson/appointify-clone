using Appointify.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Appointify.Admin.Utilities
{
    public class Authentication : TypeFilterAttribute
    {
        public Authentication(params Role[] roles) : base(typeof(AuthenticationFilter))
        {
            Arguments = new object[] { roles };
        }
    }

    public class AuthenticationFilter : IAuthorizationFilter
    {
        private readonly Role[] _roles;
        private readonly UserManager _userManager;

        public AuthenticationFilter(Role[] roles, UserManager userManager)
        {
            _roles = roles;
            _userManager = userManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_userManager.IsSignedIn())
            {
                context.Result = new RedirectToActionResult("SignIn", "Access", null);
                return;
            }

            if (_roles.Length == 0 || _userManager.IsInRoles(_roles))
                return;

            context.Result = new RedirectToActionResult("Error", "Home", new { id = 401 });
        }
    }
}
