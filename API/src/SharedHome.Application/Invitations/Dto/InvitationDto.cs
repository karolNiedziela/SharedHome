using SharedHome.Domain.Invitations.Constants;

namespace SharedHome.Application.Invitations.Dto
{
    public class InvitationDto
    {
        public int HouseGroupId { get; set; }

        public string RequestedByPersonId { get; set; } = default!;

        public string RequestedToPersonId { get; set; } = default!;

        public string InvitationStatus { get; set; } = default!;

        public string SentByFirstName { get; set; } = default!;

        public string SentByLastName { get; set; } = default!;
    }
}
