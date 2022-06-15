using SharedHome.Shared.Abstractions.Attributes;
using System.Text.Json.Serialization;

namespace SharedHome.Shared.Abstractions.Requests
{
    public abstract class AuthorizeRequest
    {
        [JsonIgnore]
        [SwaggerExclude]
        public string? PersonId { get; set; }
    }
}
