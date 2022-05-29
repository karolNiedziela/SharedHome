using System.ComponentModel.DataAnnotations;

namespace SharedHome.Infrastructure.Identity.Models
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not a valid e-mail address.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirm password do not match.")]
        public string ConfirmPassword { get; set; } = default!;

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = default!;
    }
}
