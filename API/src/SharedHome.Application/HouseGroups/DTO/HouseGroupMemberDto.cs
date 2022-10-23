using System.Text.Json.Serialization;

namespace SharedHome.Application.HouseGroups.DTO
{
    public class HouseGroupMemberDto
    {
        public Guid PersonId { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string FirstName { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string LastName { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Email { get; set; } = default!;

        public bool IsOwner { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string FullName { get; set; } = default!;
    }
}
