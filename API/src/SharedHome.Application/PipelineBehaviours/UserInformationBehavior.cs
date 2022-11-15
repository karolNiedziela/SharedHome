using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Shared.Abstractions.User;

namespace SharedHome.Application.PipelineBehaviours
{
    public class UserInformationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUser _currentUser;

        public UserInformationBehavior(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is AuthorizeRequest command)
            {
                command.PersonId = _currentUser.UserId;
                command.FirstName = _currentUser.FirstName;
                command.LastName = _currentUser.LastName;
            }

            return await next();
        }
    }
}
