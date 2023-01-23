using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands.CancelBillPayment;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Shared.ValueObjects;

using SharedHome.Tests.Shared.Providers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Handlers
{
    public class CancelBillPaymentHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;
        private readonly IRequestHandler<CancelBillPaymentCommand, Unit> _commandHandler;

        public CancelBillPaymentHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _billService = Substitute.For<IBillService>();
            _commandHandler = new CancelBillPaymentHandler(_billRepository, _billService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var bill = BillFakeProvider.Get(billCost: new Money(100m, "zł"), isPaid: true);

            _billService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(bill);

            var command = new CancelBillPaymentCommand
            {
                BillId = BillFakeProvider.BillId,
                PersonId = BillFakeProvider.PersonId
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).UpdateAsync(Arg.Is<Bill>(bill => bill.Cost == null &&
            bill.IsPaid == false));
        }
    }
}
