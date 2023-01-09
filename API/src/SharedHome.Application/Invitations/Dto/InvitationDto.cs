using System.Text.Json.Serialization;

namespace SharedHome.Application.Invitations.Dto
{
    public class InvitationDto
    {
        public Guid Id { get; set; }

        public Guid HouseGroupId { get; set; }

        public string HouseGroupName { get; set; } = default!;

        public Guid RequestedByPersonId { get; set; } = default!;

        public Guid RequestedToPersonId { get; set; } = default!;

        public string InvitationStatus { get; set; } = default!;

        public string SentByFirstName { get; set; } = default!;

        public string SentByLastName { get; set; } = default!;

        public string SentByFullName { get; set; } = default!;

        public string SentToFirstName { get; set; } = default!;

        public string SentToLastName { get; set; } = default!;

        public string SentToFullName { get; set; } = default!;
    }
}
