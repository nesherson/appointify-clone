using System.Collections.Generic;

namespace Appointify.Data.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? MinimumDuration { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
