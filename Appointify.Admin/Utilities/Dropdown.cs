using System.Linq;
using System.Threading.Tasks;
using Appointify.Data;
using Appointify.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Admin.Utilities
{
    public class Dropdown
    {
        private readonly DatabaseContext _dbContext;

        public Dropdown(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SelectList> Countries(int? selectedId = null)
        {
            var countries = await _dbContext.Countries.ToListAsync();

            var selectList = new SelectList(countries, nameof(Country.Id), nameof(Country.Name), selectedId);

            return selectList;
        }

        public async Task<SelectList> Cities(int? selectedId = null)
        {
            var cities = await _dbContext.Cities.ToListAsync();

            var selectList = new SelectList(cities, nameof(City.Id), nameof(City.Name), selectedId);

            return selectList;
        }

        public async Task<SelectList> Companies(int? selectedId = null)
        {
            var companies = await _dbContext.Companies.ToListAsync();

            var selectList = new SelectList(companies, nameof(Company.Id), nameof(Company.Name), selectedId);

            return selectList;
        }

    }
}
