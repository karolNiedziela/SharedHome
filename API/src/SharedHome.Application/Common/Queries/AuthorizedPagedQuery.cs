using SharedHome.Application.Common.Requests;
using MediatR;

namespace SharedHome.Application.Common.Queries
{
    public class AuthorizedPagedQuery<TResponse> : AuthorizeRequest, IRequest<Paged<TResponse>>
    {
        public int PageNumber { get; set; }

        // Number of elements per page
        public int PageSize { get; set; }
    }
}
