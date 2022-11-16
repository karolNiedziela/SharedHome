using MediatR;

namespace SharedHome.Application.Common.Queries
{
    public interface IPagedQuery<TResponse> : IRequest<TResponse>
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }
    }
}
