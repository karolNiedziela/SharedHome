using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands;
using SharedHome.Application.Bills.Commands.Handlers;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Application.Services;
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
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IBillService _billService;
        private readonly ICommandHandler<CancelBillPayment, Unit> _commandHandler;

        public CancelBillPaymentHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _houseGroupService = Substitute.For<IHouseGroupReadService>();
            _billService = Substitute.For<IBillService>();
            _commandHandler = new CancelBillPaymentHandler(_billRepository, _houseGroupService, _billService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_And_BillService_When_Person_Is_In_HouseGroup()
        {
            var bill = BillProvider.Get(billCost: 100, isPaid: true);

            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(true);

            _billService.GetForHouseGroupMemberAsync(Arg.Any<int>(), Arg.Any<string>()).Returns(bill);

            var command = new CancelBillPayment
            {
                BillId = 1,
                PersonId = "personId"
            };

            await _commandHandler.Handle(command, default);

            await _billService.Received(1).GetForHouseGroupMemberAsync(Arg.Any<int>(), Arg.Any<string>());
            await _billRepository.Received(1).UpdateAsync(Arg.Is<Bill>(bill => bill.Cost == null &&
            bill.IsPaid == false));
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_And_BillRepository_When_Person_Is_In_HouseGroup()
        {
            var bill = BillProvider.Get(billCost: 100, isPaid: true);

            _houseGroupService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(false);

            _billRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(bill);

            var command = new CancelBillPayment
            {
                BillId = 0,
                PersonId = "personId"
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).GetAsync(Arg.Any<int>(), Arg.Any<string>());
            await _billRepository.Received(1).UpdateAsync(Arg.Is<Bill>(bill => bill.Cost == null &&
            bill.IsPaid == false));
        }
    }
}
