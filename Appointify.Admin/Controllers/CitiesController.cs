using Microsoft.AspNetCore.Mvc;

using Appointify.Data.Entities;
using Appointify.Admin.Utilities;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Appointify.Data;
using Appointify.Admin.Resources;
using Appointify.Admin.ViewModels.Cities;

namespace Appointify.Admin.Controllers
{
    [Authentication(Role.SuperAdministrator)]
    public class CitiesController : Controller
    {

        private readonly DatabaseContext _dbContext;
        public CitiesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                Cities = await _dbContext.Cities.Include(c => c.Country).ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FilterCities(IndexViewModel viewModel)
        {
            viewModel.Cities = await _dbContext.Cities
                                .Where(c => (!string.IsNullOrWhiteSpace(viewModel.PostalCode) && c.PostalCode == viewModel.PostalCode) || c.CountryId == viewModel.CountryId)
                                .ToListAsync();   

            return View("Index", viewModel);
        }

        [HttpGet]
        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(AddViewModel viewModel)
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.PostalCode == viewModel.PostalCode);
            if (city != null)
            {
                ModelState.AddModelError(string.Empty, Strings.CityAlreadyExists);
                return View();
            }

            city = new City
            {
                Name = viewModel.Name,
                PostalCode = viewModel.PostalCode,
                CountryId = viewModel.CountryId
            };

            _dbContext.Cities.Add(city);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("AddCity");
        }

        [HttpGet]
        public async Task<IActionResult> EditCity(int id)
        {
            var city = await _dbContext.Cities.FindAsync(id);

            var viewModel = new EditViewModel
            {
                Id = city.Id,
                Name = city.Name,
                PostalCode = city.PostalCode,
                CountryId = city.CountryId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCity(EditViewModel viewModel)
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync(c => (!string.IsNullOrWhiteSpace(viewModel.PostalCode) && c.PostalCode == viewModel.PostalCode) && c.Id != viewModel.Id);

            if (city != null) 
            {
                ModelState.AddModelError(string.Empty, Strings.CityAlreadyExists);
                return View(viewModel);
            }

            city = await _dbContext.Cities.FindAsync(viewModel.Id);

            city.Name = viewModel.Name;
            city.PostalCode = viewModel.PostalCode;
            city.CountryId = viewModel.CountryId;

            _dbContext.Update(city);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCity(int id)
        {
            var city = await _dbContext.Cities.FindAsync(id);
            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
