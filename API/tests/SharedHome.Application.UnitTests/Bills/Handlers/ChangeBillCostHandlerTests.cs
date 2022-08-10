﻿using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands.ChangeBillCost;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Handlers
{
    public class ChangeBillCostHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;
        private readonly ICommandHandler<ChangeBillCostCommand, Unit> _commandHandler;

        public ChangeBillCostHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _billService = Substitute.For<IBillService>();
            _commandHandler = new ChangeBillCostHandler(_billRepository, _billService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var bill = BillProvider.Get(billCost: new Money(1000m, "zł"));

            _billService.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(bill);

            var command = new ChangeBillCostCommand
            {
                BillId = 1,
                Cost = 2000,
                Currency = "zł"
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).UpdateAsync(Arg.Is<Bill>(bill => bill.Cost!.Amount == command.Cost));
        }
    }
}
