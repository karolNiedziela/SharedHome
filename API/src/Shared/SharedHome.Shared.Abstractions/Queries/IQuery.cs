using MediatR;

namespace SharedHome.Shared.Abstractions.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }

}
