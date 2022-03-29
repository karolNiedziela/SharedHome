using MediatR;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.Bills.Commands
{
    public class AddBill : AuthorizeCommand, ICommand<Unit>
    {
        public string BillType { get; set; } = default!;

        public string ServiceProviderName { get; set; } = default!;

        public DateTime DateOfPayment { get; set; }

        public decimal Cost { get; set; }
    }
}
