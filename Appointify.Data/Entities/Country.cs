using System.Collections.Generic;

namespace Appointify.Data.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
