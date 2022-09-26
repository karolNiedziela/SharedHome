namespace SharedHome.Application.Common.DTO
{
    public abstract class AuditableDto
    {
        public string CreatedBy { get; set; } = default!;
    }
}
