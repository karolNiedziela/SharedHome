using MediatR;
using SharedHome.Identity.Authentication;

namespace SharedHome.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<AuthenticationResponse>;
}
