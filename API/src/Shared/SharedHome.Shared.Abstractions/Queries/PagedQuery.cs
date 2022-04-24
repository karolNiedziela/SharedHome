using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Shared.Abstractions.Queries
{
    public abstract class PagedQuery<TResponse> : AuthorizeRequest, IPagedQuery<Paged<TResponse>>
    {
        public int PageNumber { get; set; }

        // Number of elements per page
        public int PageSize { get; set; }
    }
}
