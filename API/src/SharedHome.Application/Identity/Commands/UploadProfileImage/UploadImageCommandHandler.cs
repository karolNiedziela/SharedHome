using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using SharedHome.Identity.Entities;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Abstractions.Commands;
using System.Globalization;

namespace SharedHome.Application.Identity.Commands.UploadProfileImage
{
    public class UploadImageCommandHandler : ICommandHandler<UploadProfileImageCommand, Unit>
    {
        private readonly Cloudinary _cloudinary;
        private readonly IIdentityService _identityService;

        public UploadImageCommandHandler(Cloudinary cloudinary, IIdentityService identityService)
        {
            _cloudinary = cloudinary;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = $"{request.FirstName}_{request.LastName}",
                    UseFilename = true,
                    UniqueFilename = false,
                    Transformation = new Transformation().Width(50).Height(50),
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

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

            await _identityService.AddUserImage(request.PersonId.ToString(), image);

            return Unit.Value;
        }
    }
}
