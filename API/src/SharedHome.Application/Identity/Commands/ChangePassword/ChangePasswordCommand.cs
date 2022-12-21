using MediatR;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Identity.Commands.ChangePassword
{
    public class ChangePasswordCommand : AuthorizeRequest, IRequest<Unit>
    {
        public string CurrentPassword { get; set; } = default!;

        public string NewPassword { get; set; } = default!;

        public string ConfirmNewPassword { get; set; } = default!;
    }
}
