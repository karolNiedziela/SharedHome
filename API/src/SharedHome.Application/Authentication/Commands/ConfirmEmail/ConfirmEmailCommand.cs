using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Authentication.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(string Email, string Code) : ICommand<Unit>;
}
