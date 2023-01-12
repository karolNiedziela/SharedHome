namespace SharedHome.Application.Common.DTO
{
    public abstract class AuditableDto
    {
        public string CreatedByFullName { get; set; } = default!;
    }
}
