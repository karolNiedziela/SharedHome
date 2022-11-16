using MediatR;
using SharedHome.Application.Common.Queries;

namespace SharedHome.Application.PipelineBehaviours
{
    public class PagedQueryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private const int MaxPageSize = 100;

        private const int MinPageSize = 10;

        private const int DefaultPageSize = 25;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IPagedQuery<TResponse> pagedQuery)
            {
                if (pagedQuery.PageNumber <= 0)
                {
                    pagedQuery.PageNumber = 1;
                }

                if (pagedQuery.PageSize <= 0)
                {
                    pagedQuery.PageSize = DefaultPageSize;
                }

                if (pagedQuery.PageSize > MaxPageSize)
                {
                    pagedQuery.PageSize = DefaultPageSize;
                }

                if (pagedQuery.PageSize < MinPageSize)
                {
                    pagedQuery.PageSize = MinPageSize;
                }
            }

            return await next();
        }
    }
}
