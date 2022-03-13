using DomainModel.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Foundation
{
    public interface IAccountService
    {
        public Task<IdentityResult> RegisterAsync(User user, string password);

        public Task<SignInResult> SignInAsync(User user, string password);

        public Task SignOutAsync();
    }
}
