using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

using AutoMapper;

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
        private readonly IMapper _mapper;

        public CountriesController(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

        [HttpGet]
        public async Task<IActionResult> FilterCountries(IndexViewModel viewModel)
        {

            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                viewModel.Countries = await _dbContext.Countries
                               .ToListAsync();
                return View("Index", viewModel);
            }

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

            if (viewModel.Name == null)
            {
                ModelState.AddModelError(string.Empty, Strings.CountryNameRequired);
                return View(viewModel);
            }

            var country = await _dbContext.Countries.FirstOrDefaultAsync(c => c.Name == viewModel.Name);
            if (country != null)
            {
                ModelState.AddModelError(string.Empty, Strings.CountryAlreadyExists);
                return View();
            }

            country = new Country();

            _mapper.Map(viewModel, country);

            await _dbContext.Countries.AddAsync(country);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("AddCountry");
        }

        [HttpGet]
        public async Task<IActionResult> EditCountry(int id)
        {
            var country = await _dbContext.Countries.FindAsync(id);

            return View(_mapper.Map<EditViewModel>(country));
        }

        [HttpPost]
        public async Task<IActionResult> EditCountry(EditViewModel viewModel)
        {
            if (viewModel.Name == null)
            {
                ModelState.AddModelError(string.Empty, Strings.CountryNameRequired);
                return View(viewModel);
            }

            var country = await _dbContext.Countries.FindAsync(viewModel.Id);

            _mapper.Map(viewModel, country);

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
