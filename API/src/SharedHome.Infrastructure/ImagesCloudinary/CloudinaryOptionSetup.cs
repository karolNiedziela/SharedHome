using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SharedHome.Infrastructure.ImagesCloudinary
{
    public class CloudinaryOptionSetup : IConfigureOptions<CloudinaryOptions>
    {
        public const string SectionName = "Cloudinary";

        private readonly IConfiguration _configuration;

        public CloudinaryOptionSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(CloudinaryOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
