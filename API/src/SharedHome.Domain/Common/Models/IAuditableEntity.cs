namespace SharedHome.Domain.Common.Models
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }

        public string CreatedByFullName { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public string? ModifiedByFullName { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}