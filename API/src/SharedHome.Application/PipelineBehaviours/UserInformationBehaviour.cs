using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.User;
using System.Security.Claims;

namespace SharedHome.Application.PipelineBehaviours
{
    public class UserInformationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUser _currentUser;

        public UserInformationBehaviour(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is AuthorizeCommand command)
            {
                command.PersonId = _currentUser.UserId;
                command.Email = _currentUser.Claims[ClaimTypes.Email].First();
                command.FirstName = _currentUser.Claims["FirstName"].First();
                command.LastName = _currentUser.Claims["LastName"].First();
            }

            return await next();
        }
    }
}
