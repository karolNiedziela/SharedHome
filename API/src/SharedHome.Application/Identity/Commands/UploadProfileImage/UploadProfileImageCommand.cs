using MediatR;
using Microsoft.AspNetCore.Http;

using SharedHome.Application.Common.Requests;
using SharedHome.Application.Identity.Dto;

namespace SharedHome.Application.Authentication.Commands.UploadProfileImage
{
    public class UploadProfileImageCommand : AuthorizeRequest, IRequest<ProfileImageDto>
    {
        public IFormFile File { get; set; } = default!;
    }
}
