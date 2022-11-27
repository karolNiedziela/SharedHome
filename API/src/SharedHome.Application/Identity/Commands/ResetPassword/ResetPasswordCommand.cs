using MediatR;

namespace SharedHome.Application.Identity.Commands.ResetPassword
{
    public record ResetPasswordCommand(string Email, string Code, string NewPassword, string ConfirmNewPassword) : IRequest<Unit>;
}
