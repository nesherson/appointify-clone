using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Appointify.Data;
using Appointify.Admin.Resources;
using Appointify.Admin.ViewModels.Cities;
using Appointify.Data.Entities;
using Appointify.Admin.Utilities;

namespace Appointify.Admin.Controllers
{
    [Authentication(Role.SuperAdministrator)]
    public class CitiesController : Controller
    {

        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        public CitiesController(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

        [HttpGet]
        public async Task<IActionResult> FilterCities(IndexViewModel viewModel)
        {

            viewModel.Cities = await _dbContext.Cities
                .Where(c => (viewModel.PostalCode.IsNotSet() || c.PostalCode == viewModel.PostalCode) && (viewModel.CountryId == null || c.CountryId == viewModel.CountryId))
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

            if (viewModel.Name == null || viewModel.PostalCode == null)
            {
                ModelState.AddModelError(string.Empty, Strings.AddCityInputsRequired);
                return View();
            }

            var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.PostalCode == viewModel.PostalCode);
            if (city != null)
            {
                ModelState.AddModelError(string.Empty, Strings.CityAlreadyExists);
                return View();
            }

            city = new City();

            _mapper.Map(viewModel, city);

            await _dbContext.Cities.AddAsync(city);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("AddCity");
        }

        [HttpGet]
        public async Task<IActionResult> EditCity(int id)
        {
            var city = await _dbContext.Cities.FindAsync(id);

            return View(_mapper.Map<EditViewModel>(city));
        }

        [HttpPost]
        public async Task<IActionResult> EditCity(EditViewModel viewModel)
        {
            var postalCodeAlreadyExists = await _dbContext.Cities.AnyAsync(c => (!string.IsNullOrWhiteSpace(viewModel.PostalCode) && c.PostalCode == viewModel.PostalCode) && c.Id != viewModel.Id);
            if (postalCodeAlreadyExists) 
            {
                ModelState.AddModelError(string.Empty, Strings.CityAlreadyExists);
                return View(viewModel);
            }

            var city = await _dbContext.Cities.FindAsync(viewModel.Id);

            _mapper.Map(viewModel, city);

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
