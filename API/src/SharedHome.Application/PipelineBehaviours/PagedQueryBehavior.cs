using MediatR;
using SharedHome.Application.Common.Queries;

namespace SharedHome.Application.PipelineBehaviours
{
    public class PagedQueryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
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
                    pagedQuery.PageSize = PagedBase.DefaultPageSize;
                }

                if (pagedQuery.PageSize > PagedBase.MaxPageSize)
                {
                    pagedQuery.PageSize = PagedBase.DefaultPageSize;
                }

                if (pagedQuery.PageSize < PagedBase.MinPageSize)
                {
                    pagedQuery.PageSize = PagedBase.MinPageSize;
                }
            }

            return await next();
        }
    }
}
