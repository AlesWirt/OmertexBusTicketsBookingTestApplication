using BLL.Foundation;
using WebApp.ViewModels;
using DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAccountService _accountService;


        public RegisterController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = new User
            {
                UserName = registerViewModel.Login,
                Email = registerViewModel.Email
            };

            var result = await _accountService.RegisterAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                var signInResult = await _accountService.SignInAsync(user, registerViewModel.Password);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt");
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerViewModel);
        }
    }
}
