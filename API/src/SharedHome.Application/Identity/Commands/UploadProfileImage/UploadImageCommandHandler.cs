using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using SharedHome.Application.Identity.Dto;
using SharedHome.Application.Identity.Exceptions;
using SharedHome.Identity.Entities;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Extensions;
using System.Globalization;

namespace SharedHome.Application.Authentication.Commands.UploadProfileImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadProfileImageCommand, ProfileImageDto>
    {
        private readonly Cloudinary _cloudinary;
        private readonly IApplicationUserService _identityService;

        public UploadImageCommandHandler(Cloudinary cloudinary, IApplicationUserService identityService)
        {
            _cloudinary = cloudinary;
            _identityService = identityService;
        }

        public async Task<ProfileImageDto> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
        {
            if (request.File.Length <= 0)
            {
                return new ProfileImageDto();
            }         
           
            if (!request.File.IsImage())
            {
                throw new InvalidImageFormatException();
            }           

            using var stream = request.File.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(request.File.FileName, stream),
                Folder = $"{request.FirstName}_{request.LastName}_{request.PersonId}",
                UseFilename = true,
                UniqueFilename = true,
                Transformation = new Transformation().Width(50).Height(50),
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            var formatProvider = CultureInfo.CreateSpecificCulture("en-US");
            var image = new UserImage
            {
                Bytes = (int)uploadResult.Bytes,
                CreatedAt = DateTime.UtcNow,
                Format = uploadResult.Format,
                Height = uploadResult.Height,
                Path = uploadResult.Url.AbsolutePath,
                PublicId = uploadResult.PublicId,
                ResourceType = uploadResult.ResourceType,
                SecureUrl = uploadResult.SecureUrl.AbsoluteUri,
                Signature = uploadResult.Signature,
                Type = uploadResult.JsonObj["type"]?.ToString()!,
                Url = uploadResult.Url.AbsoluteUri,
                Version = int.Parse(uploadResult.Version, formatProvider),
                Width = uploadResult.Width,
                UserId = request.PersonId.ToString()
            };

            await _identityService.AddUserImageAsync(request.PersonId.ToString(), image);

            return new ProfileImageDto
            {
                Url = image.Url
            };
        }
    }
}
