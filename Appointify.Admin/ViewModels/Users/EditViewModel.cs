using Appointify.Data.Entities;

namespace Appointify.Admin.ViewModels.Users
{
    public class EditViewModel
    { 
      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Username { get; set; }
      public string Email { get; set; }
      public Role Role { get; set; }
      public int CityId { get; set; }
      public int CompanyId { get; set; }
    }
}
