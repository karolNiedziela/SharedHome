using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedHome.IntegrationTests.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this HttpClient client, string? requestUri, TValue value, JsonSerializerOptions? options = null)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            
            var mediaType = new MediaTypeHeaderValue("application/json");
            var content = JsonContent.Create(value, mediaType, options);
            return client.PatchAsync(requestUri, content);
        }
    }
}
