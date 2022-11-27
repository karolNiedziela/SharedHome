using MediatR;

namespace SharedHome.Application.Identity.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(string Email) : IRequest<Unit>;
}
