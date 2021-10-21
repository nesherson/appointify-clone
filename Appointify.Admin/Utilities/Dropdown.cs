using System.Threading.Tasks;
using Appointify.Data;
using Appointify.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Admin.Utilities
{
    public class Dropdown
    {
        private readonly DatabaseContext _databaseContext;

        public Dropdown(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<SelectList> Countries(int? selectedId = null)
        {
            var countries = await _databaseContext.Countries.ToListAsync();

            var selectList = new SelectList(countries, nameof(Country.Id), nameof(Country.Name), selectedId);

            return selectList;
        }
    }
}
