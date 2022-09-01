using FluentValidation;

namespace SharedHome.Client.Modules.Authentication.Pages.Login
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");


            RuleFor(x => x.Password)
                .NotEmpty();
            
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<LoginModel>.CreateWithOptions((LoginModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
            {
                return Array.Empty<string>();
            }

            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
