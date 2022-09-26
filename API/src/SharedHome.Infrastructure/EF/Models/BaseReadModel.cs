namespace SharedHome.Infrastructure.EF.Models
{
    internal abstract class BaseReadModel
    {
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = default!;

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; } = default!;
    }
}
