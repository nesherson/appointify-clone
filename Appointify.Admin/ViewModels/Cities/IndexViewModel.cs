using System.Collections.Generic;

using Appointify.Data.Entities;

namespace Appointify.Admin.ViewModels.Cities
{
    public class IndexViewModel
    {
        public string PostalCode { get; set; }
        public int CountryId { get; set; }
        public List<City> Cities { get; set; }

    }
}
