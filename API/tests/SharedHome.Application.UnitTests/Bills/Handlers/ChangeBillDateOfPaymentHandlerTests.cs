using System;
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
    public class ChangeBillDateOfPaymentHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly ICommandHandler<ChangeBillDateOfPayment, Unit> _commandHandler;

        public ChangeBillDateOfPaymentHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _commandHandler = new ChangeBillDateOfPaymentHandler(_billRepository);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var bill = BillProvider.Get();

            _billRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                 .Returns(bill);

            var command = new ChangeBillDateOfPayment
            {
                BillId = 1,
                DateOfPayment = new DateTime(2021, 10, 10)
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).UpdateAsync(Arg.Is<Bill>(bill =>
                bill.DateOfPayment == command.DateOfPayment));
        }
    }
}
