using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands;
using SharedHome.Application.Bills.Commands.Handlers;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Handlers
{
    public class AddBillHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly ICommandHandler<AddBill, Unit> _commandHandler;

        public AddBillHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _commandHandler = new AddBillHandler(_billRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddBill
            {
                ServiceProviderName = "Bill",
                BillType = 2,
                Cost = 1500m,
                Currency = "PLN",
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).AddAsync(Arg.Any<Bill>());
        }
    }
}
