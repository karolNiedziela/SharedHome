using SharedHome.Shared.Attributes;
using System.Text.Json.Serialization;

namespace SharedHome.Application.Common.Requests
{
    public abstract class AuthorizeRequest
    {
        [JsonIgnore]
        [SwaggerExclude]
        internal Guid PersonId { get; set; } = default!;

        [JsonIgnore]
        [SwaggerExclude]
        internal string? FirstName { get; set; } = default!;

        [JsonIgnore]
        [SwaggerExclude]
        internal string? LastName { get; set; } = default!;
    }
}
