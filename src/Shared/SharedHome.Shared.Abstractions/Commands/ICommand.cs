using MediatR;

namespace SharedHome.Shared.Abstractions.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
