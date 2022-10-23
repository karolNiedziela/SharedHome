namespace SharedHome.Infrastructure.EF.Models
{
    internal class InvitationReadModel : BaseReadModel
    {
        public Guid Id { get; set; }

        public int Status { get; set; }

        public HouseGroupReadModel HouseGroup { get; set; } = default!;

        public Guid HouseGroupId { get; set; }

        public PersonReadModel RequestedByPerson { get; set; } = default!;

        public Guid RequestedByPersonId { get; set; } = default!;

        public PersonReadModel RequestedToPerson { get; set; } = default!;

        public Guid RequestedToPersonId { get; set; } = default!;
    }
}
