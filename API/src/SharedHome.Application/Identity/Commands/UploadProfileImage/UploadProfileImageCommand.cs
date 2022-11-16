using MediatR;
using Microsoft.AspNetCore.Http;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Authentication.Commands.UploadProfileImage
{
    public class UploadProfileImageCommand : AuthorizeRequest, IRequest<Unit>
    {
        public IFormFile File { get; set; } = default!;
    }
}
