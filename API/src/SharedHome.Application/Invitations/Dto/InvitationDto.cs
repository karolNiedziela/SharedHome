using SharedHome.Domain.Invitations.Constants;
using System.Text.Json.Serialization;

namespace SharedHome.Application.Invitations.Dto
{
    public class InvitationDto
    {
        public int HouseGroupId { get; set; }

        public string RequestedByPersonId { get; set; } = default!;

        public string RequestedToPersonId { get; set; } = default!;

        public string InvitationStatus { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SentByFirstName { get; set; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SentByLastName { get; set; } = default!;
    }
}
