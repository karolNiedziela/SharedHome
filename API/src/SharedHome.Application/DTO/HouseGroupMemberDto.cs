namespace SharedHome.Application.DTO
{
    public class HouseGroupMemberDto
    {
        public Guid PersonId { get; set; }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public bool IsOwner { get; set; }

        public string FullName { get; set; } = default!;
    }
}
