using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System.Text;

namespace SharedHome.Shared.Extensions
{
    public static class FileExtensions
    {
        private static readonly Dictionary<string, string> ImageMimeDictionary = new()
        {
            { ".bmp", "image/bmp" },
            { ".gif", "image/gif" },
            { ".svg", "image/svg+xml" },
            { ".jpe", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".jpg", "image/jpeg" },
            { ".png", "image/png" }
        };

        private static readonly List<string> ImageMimes = new()
        {
            { "image/bmp" },
            { "image/gif" },
            { "image/svg+xml" },
            { "image/jpeg" },
            { "image/jpeg" },
            { "image/jpeg" },
            { "image/png" }
        };

        public static bool IsImage(this IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var imageInfo = Image.Identify(stream, out var format);

            if (format is null)
            {
                return false;
            }

            return ImageMimes.Contains(format.DefaultMimeType);
        }      
    }
}
