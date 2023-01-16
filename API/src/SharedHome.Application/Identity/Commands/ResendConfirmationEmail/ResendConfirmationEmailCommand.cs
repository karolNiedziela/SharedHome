using MediatR;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Identity.Commands.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommand : IRequest<ApiResponse<string>>
    {
        public string Email { get; set; } = default!;
    }
}
