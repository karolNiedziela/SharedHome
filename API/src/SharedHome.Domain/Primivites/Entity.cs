namespace SharedHome.Domain.Primivites
{
    public abstract class Entity : IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; } = default!;

        public string CreatedByFullName { get; set; } = default!;

        public DateTime? ModifiedAt { get; set; }

        public Guid? ModifiedBy { get; set; }

        public string? ModifiedByFullName { get; set; }
    }
}
