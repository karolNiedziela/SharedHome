using System.Text.Json.Serialization;

namespace SharedHome.Shared.Abstractions.Commands
{
    public class AuthorizeCommand
    {
        [JsonIgnore]
        public string? PersonId { get; set; } = default!;

        [JsonIgnore]
        public string? FirstName { get; set; } = default!;

        [JsonIgnore]
        public string? LastName { get; set; } = default!;

        [JsonIgnore]
        public string? Email { get; set; } = default!;
    }
}
