namespace SharedHome.Application.HouseGroups.DTO
{
    public class HouseGroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public IEnumerable<HouseGroupMemberDto> Members { get; set; } = default!;
    }
}
