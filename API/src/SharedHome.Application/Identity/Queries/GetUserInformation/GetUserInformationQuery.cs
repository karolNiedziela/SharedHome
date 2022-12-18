using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Application.Identity.Dto;

namespace SharedHome.Application.Identity.Queries.GetUserInformation
{
    public class GetUserInformationQuery : AuthorizeRequest, IRequest<UserInformationDto>
    {
    }
}
