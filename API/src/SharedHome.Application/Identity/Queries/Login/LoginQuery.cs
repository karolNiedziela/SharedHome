using MediatR;
using SharedHome.Application.Common.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Identity.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IQuery<AuthenticationResponse>;
}
