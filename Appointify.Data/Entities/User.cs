using System.Collections.Generic;

namespace Appointify.Data.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public Role Role { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Company> Companies { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
