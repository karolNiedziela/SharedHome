using MediatR;
using Microsoft.AspNetCore.Http;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Identity.Commands.UploadProfileImage
{
    public class UploadProfileImageCommand : AuthorizeRequest, ICommand<Unit>
    {
        public IFormFile File { get; set; } = default!;
    }
}
