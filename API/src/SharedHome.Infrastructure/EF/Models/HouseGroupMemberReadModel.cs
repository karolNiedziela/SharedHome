namespace SharedHome.Infrastructure.EF.Models
{
    internal class HouseGroupMemberReadModel : BaseReadModel
    {
        public Guid PersonId { get; set; } = default!;

        public PersonReadModel Person { get; set; } = default!;

        public Guid HouseGroupId { get; set; }

        public HouseGroupReadModel HouseGroup { get; set; } = default!;

        public bool IsOwner { get; set; }
    }
}
