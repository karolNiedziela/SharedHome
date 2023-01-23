using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands.DeleteBill;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;

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
        private readonly IRequestHandler<DeleteBillCommand, Unit> _commandHandler;

        public DeleteBillHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _billService = Substitute.For<IBillService>();
            _commandHandler = new DeleteBillHandler(_billRepository, _billService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var bill = BillFakeProvider.Get();

            _billService.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
              .Returns(bill);

            var command = new DeleteBillCommand
            {
                BillId = BillFakeProvider.BillId               
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).DeleteAsync(Arg.Any<Bill>());
        }
    }
}
