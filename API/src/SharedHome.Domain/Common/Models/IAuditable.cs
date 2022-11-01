namespace SharedHome.Domain.Common.Models
{
    public interface IAuditable
    {
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set;  }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }
    }
}