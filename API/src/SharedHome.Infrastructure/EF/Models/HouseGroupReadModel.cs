namespace SharedHome.Infrastructure.EF.Models
{
    internal class HouseGroupReadModel : BaseReadModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public ICollection<HouseGroupMemberReadModel> Members { get; set; } = default!;

        public ICollection<InvitationReadModel> Invitations { get; set; } = default!;
    }
}
