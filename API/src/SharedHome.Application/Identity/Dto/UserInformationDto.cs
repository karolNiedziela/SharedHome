namespace SharedHome.Application.Identity.Dto
{
    public class UserInformationDto
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? ProfileImageUrl { get; set; } = default!;
    }
}
