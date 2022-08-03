using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Authentication.Commands.Register
{
    public record RegisterCommand(string Email, string FirstName, string LastName, string Password) : ICommand<Response<string>>;
}
