using DomainModel.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Write login")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: User.UserNameMaxLength,
            MinimumLength = User.UserNameMinLength,
            ErrorMessage = "Wrong name")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Write password")]
        [DataType(DataType.Password)]
        [StringLength(User.UserPasswordMaxLength,
            MinimumLength = User.UserPasswordMinLength,
            ErrorMessage = "Wrong password")]
        public string Password { get; set; }
    }
}
