using MediatR;


namespace SharedHome.Application.Authentication.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(string Email, string Code) : IRequest<Unit>;
}
