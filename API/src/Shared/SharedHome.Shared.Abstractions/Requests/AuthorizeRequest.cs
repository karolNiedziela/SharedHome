using SharedHome.Shared.Abstractions.Attributes;
using System.Text.Json.Serialization;

namespace SharedHome.Shared.Abstractions.Requests
{
    public class AuthorizeRequest
    {       
        [JsonIgnore]
        [SwaggerExclude]
        public string? PersonId { get; set; } = default!;

        [JsonIgnore]
        [SwaggerExclude]
        public string? FirstName { get; set; } = default!;

        [JsonIgnore]
        [SwaggerExclude]
        public string? LastName { get; set; } = default!;

        [JsonIgnore]
        [SwaggerExclude]
        public string? Email { get; set; } = default!;
    }
}
