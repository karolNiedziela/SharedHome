namespace SharedHome.Infrastructure.EF.Models
{
    internal class BillReadModel : BaseReadModel
    {
        public Guid Id { get; set; }

        public int BillType { get; set; }

        public string ServiceProviderName { get; set; } = default!;

        public bool IsPaid { get; set; }
        
        public decimal? Cost { get; set; }

        public string? Currency { get; set; }

        public DateTime DateOfPayment { get; set; }

        public Guid PersonId { get; set; } = default!;

        public PersonReadModel Person { get; set; } = default!;
    }
}
