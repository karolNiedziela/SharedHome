namespace SharedHome.Infrastructure.EF.Models
{
    internal class InvitationReadModel : BaseReadModel
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public HouseGroupReadModel HouseGroup { get; set; } = default!;

        public int HouseGroupId { get; set; }

        public PersonReadModel RequestedByPerson { get; set; } = default!;

        public string RequestedByPersonId { get; set; } = default!;

        public PersonReadModel RequestedToPerson { get; set; } = default!;

        public string RequestedToPersonId { get; set; } = default!;
    }
}
