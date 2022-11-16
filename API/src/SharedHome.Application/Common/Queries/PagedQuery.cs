namespace SharedHome.Application.Common.Queries
{
    public class PagedQuery<TResponse> : IPagedQuery<Paged<TResponse>>
    {
        public int PageNumber { get; set; }

        // Number of elements per page
        public int PageSize { get; set; }
    }
}
