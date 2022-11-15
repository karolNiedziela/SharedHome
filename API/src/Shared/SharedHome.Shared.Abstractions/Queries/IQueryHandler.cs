using MediatR;

namespace SharedHome.Application.Common.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}
