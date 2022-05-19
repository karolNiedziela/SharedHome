using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands;
using SharedHome.Application.Bills.Commands.Handlers;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Shared.Abstractions.Commands;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Handlers
{
    public class ChangeBillCostHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;
        private readonly ICommandHandler<ChangeBillCost, Unit> _commandHandler;

        public ChangeBillCostHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _billService = Substitute.For<IBillService>();
            _commandHandler = new ChangeBillCostHandler(_billRepository, _billService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var bill = BillProvider.Get(billCost: 1000);

            _billService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(bill);

            var command = new ChangeBillCost
            {
                BillId = 1,
                Cost = 2000
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).UpdateAsync(Arg.Is<Bill>(bill => bill.Cost == command.Cost));
        }
    }
}
