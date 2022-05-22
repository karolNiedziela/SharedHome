using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Shared.Abstractions.Queries
{
    public abstract class PagedQuery<TResponse> : IPagedQuery<Paged<TResponse>>
    {
        public int PageNumber { get; set; }

        // Number of elements per page
        public int PageSize { get; set; }
    }
}
