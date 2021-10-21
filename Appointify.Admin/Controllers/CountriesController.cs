using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

using Appointify.Data.Entities;
using Appointify.Admin.Utilities;
using Appointify.Admin.ViewModels.Countries;
using Appointify.Data;
using Appointify.Admin.Resources;

namespace Appointify.Admin.Controllers
{
    [Authentication(Role.SuperAdministrator)]
    public class CountriesController : Controller
    {

        private readonly DatabaseContext _dbContext;
        public CountriesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                Countries = await _dbContext.Countries.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FilterCountries(IndexViewModel viewModel)
        {
            viewModel.Countries = await _dbContext.Countries
                                .Where(c => !string.IsNullOrWhiteSpace(viewModel.Name) && c.Name.Contains(viewModel.Name))
                                .ToListAsync();

            return View("Index", viewModel);
        }

        [HttpGet]
        public IActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(AddViewModel viewModel)
        {
            var country = await _dbContext.Countries.FirstOrDefaultAsync(c => c.Name == viewModel.Name);
            if (country != null)
            {
                ModelState.AddModelError(string.Empty, Strings.CountryAlreadyExists);
                return View();
            }

            country = new Country
            {
                Name = viewModel.Name
            };

            _dbContext.Countries.Add(country);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("AddCountry");
        }

        [HttpGet]
        public async Task<IActionResult> EditCountry(int id)
        {
            var country = await _dbContext.Countries.FindAsync(id);

            var viewModel = new EditViewModel
            {
                Id = country.Id,
                Name = country.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCountry(EditViewModel viewModel)
        {
            var country = await _dbContext.Countries.FirstOrDefaultAsync(c => (!string.IsNullOrWhiteSpace(viewModel.Name) && c.Name == viewModel.Name));

            if (country != null)
            {
                ModelState.AddModelError(string.Empty, Strings.CountryAlreadyExists);
                return View(viewModel);
            }

            country = await _dbContext.Countries.FindAsync(viewModel.Id);
            country.Name = viewModel.Name;
            _dbContext.Update(country);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCountry(int id)
        {
            var country = await _dbContext.Countries.FindAsync(id);
            _dbContext.Countries.Remove(country);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
