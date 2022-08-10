using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands.PayForBill;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Handlers
{
    public class PayForBillHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;
        private readonly ICommandHandler<PayForBillCommand, Unit> _commandHandler;

        public PayForBillHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _billService = Substitute.For<IBillService>();
            _commandHandler = new PayForBillHandler(_billRepository, _billService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var bill = BillProvider.Get();

            _billService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
              .Returns(bill);

            var command = new PayForBillCommand
            {
                BillId = 1,
                Cost = 150,
                Currency = "zł"
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).UpdateAsync(Arg.Is<Bill>(bill => bill.Cost!.Amount == 150 &&
            bill.IsPaid == true));
        }
    }
}
