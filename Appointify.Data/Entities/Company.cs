using System.Collections.Generic;

namespace Appointify.Data.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<ServiceCategory> ServiceCategories { get; set; }
    }
}
