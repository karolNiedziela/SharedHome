using MediatR;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IQuery<AuthenticationResponse>;
}
