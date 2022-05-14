namespace SharedHome.Infrastructure.EF.Models
{
    internal class HouseGroupReadModel : BaseReadModel
    {
        public int Id { get; set; }

        public ICollection<HouseGroupMemberReadModel> Members { get; set; } = default!;

        public ICollection<InvitationReadModel> Invitations { get; set; } = default!;
    }
}
