namespace SharedHome.Infrastructure.EF.Models
{
    internal class BillReadModel : BaseReadModel
    {
        public int Id { get; set; }

        public int BillType { get; set; }

        public string ServiceProviderName { get; set; } = default!;

        public bool IsPaid { get; set; }
        
        public decimal? Cost { get; set; }

        public string Currency { get; set; } = default!;

        public DateTime DateOfPayment { get; set; }

        public string PersonId { get; set; } = default!;

        public PersonReadModel Person { get; set; } = default!;
    }
}
