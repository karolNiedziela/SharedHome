using MediatR;
using NSubstitute;
using SharedHome.Application.ReadServices;
using SharedHome.Application.ShoppingLists.Events;
using SharedHome.Domain.ShoppingLists.Events;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;
using SharedHome.Tests.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Events
{
    public class ShoppingListCreatedEventHandlerTests
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;
        private readonly INotificationHandler<ShoppingListCreated> _notificationHandler;

        public ShoppingListCreatedEventHandlerTests()
        {            
            _houseGroupReadService = Substitute.For<IHouseGroupReadService>();
            _appNotificationService = Substitute.For<IAppNotificationService>();
            _notificationHandler = new ShoppingListCreatedEventHandler(_houseGroupReadService, _appNotificationService);
        }

        [Fact]
        public async Task Handle_Should_Do_Nothing_When_Person_Is_Not_In_HouseGroup()
        {
            var shoppingListCreated = new ShoppingListCreated(ShoppingListFakeProvider.ShoppingListId, "Test", Guid.NewGuid());

            _houseGroupReadService.IsPersonInHouseGroupAsync(Arg.Any<Guid>())
                .Returns(false);

            await _notificationHandler.Handle(shoppingListCreated, default);

            await _appNotificationService.ReceivedWithAnyArgs(0).AddAsync(Arg.Any<AppNotification>());
            await _appNotificationService.ReceivedWithAnyArgs(0).BroadcastNotificationAsync(Arg.Any<AppNotification>(), Arg.Any<Guid>(), Arg.Any<Guid>());
        }

        [Fact]
        public async Task Handle_Should_Do_Nothing_When_Only_One_Person_In_HouseGroup()
        {
            var shoppingListCreated = new ShoppingListCreated(ShoppingListFakeProvider.ShoppingListId, "Test", Guid.NewGuid());

            _houseGroupReadService.IsPersonInHouseGroupAsync(Arg.Any<Guid>())
                .Returns(true);

            _houseGroupReadService.GetMemberPersonIdsExcludingCreatorAsync(Arg.Any<Guid>())
                .Returns(Array.Empty<Guid>());

            await _notificationHandler.Handle(shoppingListCreated, default);

            await _appNotificationService.ReceivedWithAnyArgs(0).AddAsync(Arg.Any<AppNotification>());
            await _appNotificationService.ReceivedWithAnyArgs(0).BroadcastNotificationAsync(Arg.Any<AppNotification>(), Arg.Any<Guid>(), Arg.Any<Guid>());
        }

        [Fact]
        public async Task Handle_Should_Call_AddAsync_And_BroadcastNotificationAsync_OnSuccess()
        {
            var shoppingListCreated = new ShoppingListCreated(ShoppingListFakeProvider.ShoppingListId, "Test", Guid.NewGuid());

            _houseGroupReadService.IsPersonInHouseGroupAsync(Arg.Any<Guid>())
                .Returns(true);

            _houseGroupReadService.GetMemberPersonIdsExcludingCreatorAsync(Arg.Any<Guid>())
               .Returns(new List<Guid>
               {
                   Guid.NewGuid(),
                   Guid.NewGuid(),                   
               });

            await _notificationHandler.Handle(shoppingListCreated, default);

            await _appNotificationService.Received(2).AddAsync(Arg.Any<AppNotification>());
            await _appNotificationService.Received(2).BroadcastNotificationAsync(Arg.Any<AppNotification>(), Arg.Any<Guid>(), Arg.Any<Guid>());
        }
    }
}
