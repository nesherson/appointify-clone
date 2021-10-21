using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

using Appointify.Data;
using Appointify.Admin.ViewModels.Users;

namespace Appointify.Admin.Controllers
{
    public class UsersController : Controller
    {

        private readonly DatabaseContext _dbContext;

        public UsersController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

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
    }
}
