using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Appointify.Data;
using Appointify.Admin.Resources;
using Appointify.Admin.Utilities;
using Appointify.Admin.ViewModels.Access;
using Appointify.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Admin.Controllers
{
    public class AccessController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager _userManager;

        public AccessController(DatabaseContext databaseContext, UserManager userManager)
        {
            _databaseContext = databaseContext;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            var userSignedIn = _userManager.IsSignedIn();
            if (!userSignedIn)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }


            var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Username == viewModel.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, Strings.WrongCredentials);
                return View();
            }

            if (!Cryptography.Hash.Validate(viewModel.Password, user.PasswordSalt, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, Strings.WrongCredentials);
                return View();
            }

            _databaseContext.Users.Update(user);
            await _databaseContext.SaveChangesAsync();

            _userManager.SignIn(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public new IActionResult SignOut()
        {
            _userManager.SignOut();

            return RedirectToAction("SignIn");
        }
    }
}
