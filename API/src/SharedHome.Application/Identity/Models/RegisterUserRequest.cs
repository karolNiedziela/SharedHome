using System.ComponentModel.DataAnnotations;

namespace SharedHome.Application.Identity.Models
{
    public class RegisterUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = default!;

        [Required]
        public string FirstName { get; set; } = default!;

        [Required]
        public string LastName { get; set; } = default!;
    }
}
