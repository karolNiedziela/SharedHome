﻿namespace SharedHome.Infrastructure.EF.Models
{
    internal class BillReadModel : BaseReadModel
    {
        public int Id { get; set; }

        public string BillType { get; set; } = default!;

        public string ServiceProviderName { get; set; } = default!;

        public bool IsPaid { get; set; }
        
        public decimal? Cost { get; set; }

        public DateTime DateOfPayment { get; set; }

        public string PersonId { get; set; } = default!;

        public PersonReadModel Person { get; set; } = default!;
    }
}
