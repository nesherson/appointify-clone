using System.Collections.Generic;

namespace Appointify.Data.Entities
{
    public class ServiceCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
