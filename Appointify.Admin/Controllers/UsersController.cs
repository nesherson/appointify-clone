using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;

using Appointify.Data;
using Appointify.Admin.ViewModels.Users;
using Appointify.Admin.Resources;
using Appointify.Shared.Utilities;
using Appointify.Data.Entities;

namespace Appointify.Admin.Controllers
{
    public class UsersController : Controller
    {

        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;


        public UsersController(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var viewModel = new IndexViewModel
            {
                Users = await _dbContext.Users
                .Where(u => u.Role != Data.Entities.Role.SuperAdministrator)
                .Include(u => u.City)
                .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddViewModel viewModel)
        {

            if (viewModel.Username == null || viewModel.Email == null)
            {
                ModelState.AddModelError(string.Empty, Strings.AddUserInputRequired);
                return View();
            }

            var userAlreadyExists = await _dbContext.Users.AnyAsync(u => u.Username == viewModel.Username || u.Email == viewModel.Email);
            if (userAlreadyExists)
            { 
                ModelState.AddModelError(string.Empty, Strings.UserAlreadyExists);
                return View();
            }


            var salt = Cryptography.Salt.Create();
            var hash = Cryptography.Hash.Create(viewModel.Password, salt);

            var user = new User
            {
                PasswordSalt = salt,
                PasswordHash = hash
            };

            _mapper.Map(viewModel, user);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return View();
        }

        public async Task<JsonResult> GetCompanies()
        {
            var companies = await _dbContext.Companies.ToListAsync();

            return Json(companies);
        }

    }
}
