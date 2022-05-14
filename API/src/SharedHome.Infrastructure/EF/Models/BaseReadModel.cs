namespace SharedHome.Infrastructure.EF.Models
{
    internal abstract class BaseReadModel
    {
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
