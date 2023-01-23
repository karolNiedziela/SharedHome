using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Events;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Bills.Events;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;
using SharedHome.Tests.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SSharedHome.Application.UnitTests.Bills.Events
{
    public class BillCreatedEventHandlerTests
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;
        private readonly INotificationHandler<BillCreated> _notificationHandler;

        public BillCreatedEventHandlerTests()
        {
            _houseGroupReadService = Substitute.For<IHouseGroupReadService>();
            _appNotificationService = Substitute.For<IAppNotificationService>();
            _notificationHandler = new BillCreatedEventHandler(_houseGroupReadService, _appNotificationService);
        }

        [Fact]
        public async Task Handle_Should_Do_Nothing_When_Person_Is_Not_In_HouseGroup()
        {
            var billCreated = new BillCreated(BillFakeProvider.BillId, "Test", new DateOnly(), Guid.NewGuid());

            _houseGroupReadService.IsPersonInHouseGroupAsync(Arg.Any<Guid>()).Returns(false);

            await _notificationHandler.Handle(billCreated, default);

            await _appNotificationService.ReceivedWithAnyArgs(0).AddAsync(Arg.Any<AppNotification>());
            await _appNotificationService.ReceivedWithAnyArgs(0).BroadcastNotificationAsync(Arg.Any<AppNotification>(), Arg.Any<Guid>(), Arg.Any<Guid>());
        }

        [Fact]
        public async Task Handle_Should_Do_Nothing_When_Only_One_Person_In_HouseGroup()
        {
            var billCreated = new BillCreated(BillFakeProvider.BillId, "Test", new DateOnly(), Guid.NewGuid());

            _houseGroupReadService.IsPersonInHouseGroupAsync(Arg.Any<Guid>())
                .Returns(true);

            _houseGroupReadService.GetMemberPersonIdsExcludingCreatorAsync(Arg.Any<Guid>())
                .Returns(Array.Empty<Guid>());

            await _notificationHandler.Handle(billCreated, default);

            await _appNotificationService.ReceivedWithAnyArgs(0).AddAsync(Arg.Any<AppNotification>());
            await _appNotificationService.ReceivedWithAnyArgs(0).BroadcastNotificationAsync(Arg.Any<AppNotification>(), Arg.Any<Guid>(), Arg.Any<Guid>());
        }

        [Fact]
        public async Task Handle_Should_Call_AddAsync_And_BroadcastNotificationAsync_OnSuccess()
        {
            var billCreated = new BillCreated(BillFakeProvider.BillId, "Test", new DateOnly(), Guid.NewGuid());

            _houseGroupReadService.IsPersonInHouseGroupAsync(Arg.Any<Guid>())
                .Returns(true);

            _houseGroupReadService.GetMemberPersonIdsExcludingCreatorAsync(Arg.Any<Guid>())
               .Returns(new List<Guid>
               {
                   Guid.NewGuid(),
                   Guid.NewGuid(),
               });

            await _notificationHandler.Handle(billCreated, default);

            await _appNotificationService.Received(2).AddAsync(Arg.Any<AppNotification>());
            await _appNotificationService.Received(2).BroadcastNotificationAsync(Arg.Any<AppNotification>(), Arg.Any<Guid>(), Arg.Any<Guid>());
        }
    }
}
