using System.Text.Json.Serialization;

namespace SharedHome.Application.Invitations.Dto
{
    public class InvitationDto
    {
        public Guid HouseGroupId { get; set; }

        public string HouseGroupName { get; set; } = default!;

        public Guid RequestedByPersonId { get; set; } = default!;

        public Guid RequestedToPersonId { get; set; } = default!;

        public string InvitationStatus { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SentByFirstName { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SentByLastName { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SentByFullName { get; set; } = default!;
    }
}
