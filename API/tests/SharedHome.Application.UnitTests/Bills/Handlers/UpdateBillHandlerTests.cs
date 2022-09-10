﻿using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Commands.UpdateBill;
using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.AddHouseGroup
{
    public class UpdateBillHandlerTests
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;
        private readonly ICommandHandler<UpdateBillCommand, Unit> _commandHandler;

        public UpdateBillHandlerTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _billService = Substitute.For<IBillService>();
            _commandHandler = new UpdateBillHandler(_billRepository, _billService);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var bill = BillProvider.Get(billCost: new Money(100m, "zł"), isPaid: true);

            _billService.GetAsync(Arg.Any<int>(), Arg.Any<string>()).Returns(bill);

            var command = new UpdateBillCommand
            {
                Id = 0,
                PersonId = BillProvider.PersonId,
                BillType = 1,
                Cost = new MoneyDto(200, "zł"),
                DateOfPayment = bill.DateOfPayment,
                ServiceProviderName = "PGE",
            };

            await _commandHandler.Handle(command, default);

            await _billRepository.Received(1).UpdateAsync(Arg.Is<Bill>(x =>
            x.BillType == BillType.Other &&
            x.Cost != null ?  x.Cost!.Amount == 200 : x.Cost == null &&
            x.ServiceProvider.Name == "PGE"));
        }
    }
}
