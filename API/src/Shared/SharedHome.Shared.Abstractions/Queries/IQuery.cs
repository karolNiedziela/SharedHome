using MediatR;

namespace SharedHome.Application.Common.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }

}
