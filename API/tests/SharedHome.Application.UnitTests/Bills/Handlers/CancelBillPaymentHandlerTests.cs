using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands;
using SharedHome.Application.Bills.Commands.Handlers;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Handlers
{
    public class CancelBillPaymentHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly ICommandHandler<CancelBillPayment, Unit> _commandHandler;

        public CancelBillPaymentHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _commandHandler = new CancelBillPaymentHandler(_billRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_On_Success()
        {
            var bill = BillProvider.Get(billCost: 100, isPaid: true);

            _billRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(bill);

            var command = new CancelBillPayment
            {
                BillId = 1,
                PersonId = "personId"
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).UpdateAsync(Arg.Is<Bill>(bill => bill.Cost == null &&
            bill.IsPaid == false));
        }
    }
}
