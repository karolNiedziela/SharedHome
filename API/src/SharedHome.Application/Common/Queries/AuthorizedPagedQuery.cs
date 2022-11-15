using SharedHome.Application.Common.Requests;
using SharedHome.Application.Common.Queries;

namespace SharedHome.Application.Common.Queries
{
    public class AuthorizedPagedQuery<TResponse> : AuthorizeRequest, IPagedQuery<Paged<TResponse>>
    {
        public int PageNumber { get; set; }

        // Number of elements per page
        public int PageSize { get; set; }
    }
}
