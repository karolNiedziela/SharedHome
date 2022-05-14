namespace SharedHome.Application.DTO
{
    public class HouseGroupMemberDto
    {
        public string PersonId { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public bool IsOwner { get; set; }

        public string FullName { get; set; } = default!;
    }
}
