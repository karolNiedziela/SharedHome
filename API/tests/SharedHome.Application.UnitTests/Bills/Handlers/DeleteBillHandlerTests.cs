using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands.DeleteBill;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Handlers
{
    public class DeleteBillHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;
        private readonly ICommandHandler<DeleteBillCommand, Unit> _commandHandler;

        public DeleteBillHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _billService = Substitute.For<IBillService>();
            _commandHandler = new DeleteBillHandler(_billRepository, _billService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var bill = BillProvider.Get();

            _billService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
              .Returns(bill);

            var command = new DeleteBillCommand
            {
                BillId = BillProvider.BillId               
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).DeleteAsync(Arg.Any<Bill>());
        }
    }
}
