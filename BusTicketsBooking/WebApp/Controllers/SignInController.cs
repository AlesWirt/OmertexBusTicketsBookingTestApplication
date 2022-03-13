using DomainModel;
using BLL.Foundation;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class SignInController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserManagementService _userManagementService;


        public SignInController(IAccountService accountService,
            IUserManagementService userManagementService)
        {
            _accountService = accountService;
            _userManagementService = userManagementService;
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signInViewModel);
            }

            var user = await _userManagementService.GetUserByUserNameAsync(signInViewModel.UserName);

            if(user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt");

                return View(signInViewModel);
            }

            var result = await _accountService.SignInAsync(user, signInViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid password");

            return View(signInViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignOutAsync()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("SignIn", "SignIn");
        }
    }
}
