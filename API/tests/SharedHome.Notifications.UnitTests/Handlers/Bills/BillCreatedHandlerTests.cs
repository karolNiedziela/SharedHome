using MediatR;
using NSubstitute;
using SharedHome.Application.Bills.Events;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.Common.Events;
using SharedHome.Application.ReadServices;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Handlers.Bills;
using SharedHome.Notifications.Repositories;
using SharedHome.Notifications.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Notifications.UnitTests.Handlers.Bills
{
    public class BillCreatedHandlerTests
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;
        private readonly INotificationHandler<DomainEventNotification<BillCreated>> _notificationHandler;

        public BillCreatedHandlerTests()
        {
            _notificationRepository = Substitute.For<INotificationRepository>();
            _houseGroupReadService = Substitute.For<IHouseGroupReadService>();
            _appNotificationService = Substitute.For<IAppNotificationService>();
            _notificationHandler = new BillCreatedHandler(_notificationRepository, _houseGroupReadService, _appNotificationService);
        }

        [Fact]
        public async Task Handle_Should_Do_Nothing_When_Person_Is_Not_In_HouseGroup()
        {
            var billCreated = new BillCreated(1, "Test", new CreatorDto("", "", ""));

            var domainEvent = new DomainEventNotification<BillCreated>(billCreated);

            _houseGroupReadService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(false);

            await _notificationHandler.Handle(domainEvent, default);

            await _notificationRepository.ReceivedWithAnyArgs(0).AddAsync(Arg.Any<AppNotification>());
            await _appNotificationService.ReceivedWithAnyArgs(0).BroadcastNotificationAsync(Arg.Any<AppNotification>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async Task Handle_Should_Do_Nothing_When_Only_One_Person_In_HouseGroup()
        {
            var billCreated = new BillCreated(1, "Test", new CreatorDto("", "", ""));

            var domainEvent = new DomainEventNotification<BillCreated>(billCreated);

            _houseGroupReadService.IsPersonInHouseGroup(Arg.Any<string>())
                .Returns(true);

            _houseGroupReadService.GetMemberPersonIdsExcludingCreator(Arg.Any<string>())
                .Returns(Array.Empty<string>());

            await _notificationHandler.Handle(domainEvent, default);

            await _notificationRepository.ReceivedWithAnyArgs(0).AddAsync(Arg.Any<AppNotification>());
            await _appNotificationService.ReceivedWithAnyArgs(0).BroadcastNotificationAsync(Arg.Any<AppNotification>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async Task Handle_Should_Call_AddAsync_And_BroadcastNotificationAsync_OnSuccess()
        {
            var billCreated = new BillCreated(1, "Test", new CreatorDto("", "", ""));

            var domainEvent = new DomainEventNotification<BillCreated>(billCreated);

            _houseGroupReadService.IsPersonInHouseGroup(Arg.Any<string>())
                .Returns(true);

            _houseGroupReadService.GetMemberPersonIdsExcludingCreator(Arg.Any<string>())
               .Returns(new List<string>
               {
                   "1",
                   "2"
               });

            await _notificationHandler.Handle(domainEvent, default);

            await _notificationRepository.Received(2).AddAsync(Arg.Any<AppNotification>());
            await _appNotificationService.Received(2).BroadcastNotificationAsync(Arg.Any<AppNotification>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
