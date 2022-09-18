namespace SharedHome.Infrastructure.EF.Models
{
    internal class PersonReadModel : BaseReadModel
    {
        public string Id { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public ICollection<ShoppingListReadModel> ShoppingLists { get; set; } = default!;

        public ICollection<InvitationReadModel> SentInvitations { get; set; } = default!;

        public ICollection<InvitationReadModel> ReceivedInvitations { get; set; } = default!;

        public ICollection<BillReadModel> Bills { get; set; } = default!;

        public HouseGroupMemberReadModel? HouseGroupMember { get; set; } = default!;
    }
}
