using Common;
using DomainModel;
using DomainModel.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BLL.Foundation
{
    public class AccountService : IAccountService
    {
        private readonly ILog _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountService(ILog logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IdentityResult> RegisterAsync(User user, string password)
        {
            if(user == null)
            {
                _logger.LogError($"User in {typeof(AccountService).GetMethod("RegistedAsync").Name} method can not be null.");
                throw new ArgumentNullException($"User in {typeof(AccountService).GetMethod("RegistedAsync").Name} method can not be null.");
            }

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, RoleNames.User);
            }

            return result;
        }

        public async Task<SignInResult> SignInAsync(User user, string password)
        {
            if(user == null)
            {
                _logger.LogError($"User in {typeof(AccountService).GetMethod("SignInAsync").Name} method can not be null.");
                throw new ArgumentNullException($"User in {typeof(AccountService).GetMethod("SignInAsync").Name} method can not be null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                _logger.LogError($"Password in {typeof(AccountService).GetMethod("SignInAsync").Name} method can not be null.");
                throw new ArgumentNullException($"Password in {typeof(AccountService).GetMethod("SignInAsync").Name} method can not be null.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
