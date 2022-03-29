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
    public class DeleteBillHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly ICommandHandler<DeleteBill, Unit> _commandHandler;

        public DeleteBillHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _commandHandler = new DeleteBillHandler(_billRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var bill = BillProvider.Get();

            _billRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>())
              .Returns(bill);

            var command = new DeleteBill
            {
                BillId = 1                
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).DeleteAsync(Arg.Any<Bill>());
        }
    }
}
