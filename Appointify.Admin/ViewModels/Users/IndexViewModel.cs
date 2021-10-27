using System.Collections.Generic;

using Appointify.Data.Entities;

namespace Appointify.Admin.ViewModels.Users
{
    public class IndexViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; }

        public List<User> Users;
    }
}
