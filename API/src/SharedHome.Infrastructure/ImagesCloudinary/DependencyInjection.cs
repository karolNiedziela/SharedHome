using CloudinaryDotNet;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SharedHome.Infrastructure.ImagesCloudinary
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCloudinary(this IServiceCollection services)
        {
            services.ConfigureOptions<CloudinaryOptionSetup>();

            var serviceProvider = services.BuildServiceProvider();
            var cloudinaryOptions = serviceProvider.GetService<IOptions<CloudinaryOptions>>()!.Value;
            services.AddSingleton(new Cloudinary(new Account(cloudinaryOptions.CloudName, cloudinaryOptions.ApiKey, cloudinaryOptions.ApiSecret)));

            return services;
        }
    }
}
