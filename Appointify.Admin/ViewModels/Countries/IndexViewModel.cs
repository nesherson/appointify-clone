using System.Collections.Generic;

using Appointify.Data.Entities;

namespace Appointify.Admin.ViewModels.Countries
{
    public class IndexViewModel
    { 
        public string Name { get; set; } 
        public List<Country> Countries { get; set; }
    }
}
