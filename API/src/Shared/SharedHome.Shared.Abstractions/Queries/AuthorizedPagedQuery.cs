using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Shared.Abstractions.Queries
{
    public class AuthorizedPagedQuery<TResponse> : AuthorizeRequest, IPagedQuery<Paged<TResponse>>
    {
        public int PageNumber { get; set; }

        // Number of elements per page
        public int PageSize { get; set; }
    }
}
