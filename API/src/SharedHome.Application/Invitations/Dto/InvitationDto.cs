using SharedHome.Domain.Invitations.Constants;

namespace SharedHome.Application.Invitations.Dto
{
    public class InvitationDto
    {
        public int HouseGroupId { get; set; }

        public string PersonId { get; set; } = default!;

        public InvitationStatus InvitationStatus { get; set; }

        public string SentByFirstName { get; set; } = default!;

        public string SentByLastName { get; set; } = default!;
    }
}
