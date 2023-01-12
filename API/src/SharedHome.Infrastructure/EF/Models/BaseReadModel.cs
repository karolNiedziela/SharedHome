namespace SharedHome.Infrastructure.EF.Models
{
    internal abstract class BaseReadModel
    {
        public DateTime CreatedAt { get; set; }

        public string CreatedByFullName { get; set; } = default!;

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public string? ModifiedByFullName { get; set; } = default!;

        public Guid? ModifiedBy { get; set; } = default!;
    }
}
