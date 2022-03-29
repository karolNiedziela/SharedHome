namespace SharedHome.Application.DTO
{
    public class HouseGroupDto
    {
        public int Id { get; set; }

        public IEnumerable<HouseGroupMemberDto> Members { get; set; } = default!;
    }
}
