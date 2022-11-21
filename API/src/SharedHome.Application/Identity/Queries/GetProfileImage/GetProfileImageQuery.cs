using MediatR;
using SharedHome.Application.Common.Requests;
using SharedHome.Application.Identity.Dto;

namespace SharedHome.Application.Identity.Queries.GetProfileImage
{
    public class GetProfileImage : AuthorizeRequest, IRequest<ProfileImageDto>
    {

    }
}
