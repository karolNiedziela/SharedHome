using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Bills.Commands
{
    public class UpdateBill : AuthorizeRequest, ICommand<Unit>
    {
        public int Id { get; set; }

        public int BillType { get; set; }

        public string ServiceProviderName { get; set; } = default!;

        public DateTime DateOfPayment { get; set; }

        public decimal Cost { get; set; }
    }
}
