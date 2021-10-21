using System.Linq;
using Appointify.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Appointify.Admin.Utilities
{
    public class UserManager
    {
        private const string _loggedUserKey = "logged_user";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public User Get()
        {
            return _httpContextAccessor.HttpContext?.Session.GetObject<User>(_loggedUserKey);
        }

        public void SignIn(User user)
        {
            user.PasswordHash = null;
            user.PasswordSalt = null;

            _httpContextAccessor.HttpContext?.Session.SetObject(_loggedUserKey, user);
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }

        public bool IsInRoles(params Role[] roles)
        {
            var user = Get();
            if (user == null)
                return false;

            return roles.Contains(user.Role);
        }

        public bool IsSignedIn()
        {
            return _httpContextAccessor.HttpContext?.Session.HasKey(_loggedUserKey) ?? false;
        }
    }
}
