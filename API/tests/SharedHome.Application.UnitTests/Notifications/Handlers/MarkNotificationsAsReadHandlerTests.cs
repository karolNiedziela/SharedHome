using MediatR;
using NSubstitute;
using SharedHome.Application.Notifications.Commands.MarkNotificationsAsRead;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Notifications.Handlers
{
    public class MarkNotificationsAsReadHandlerTests
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICommandHandler<MarkNotificationsAsReadCommand, Unit> _commandHandler;


        public MarkNotificationsAsReadHandlerTests()
        {
            _notificationRepository =  Substitute.For<INotificationRepository>();
            _commandHandler = new MarkNotificationsAsReadHandler(_notificationRepository);
        }

        [Fact]
        public async Task Handle_Should_Change_IsRead_To_True_For_Given_NotificationIds()
        {
            var notifications = new List<AppNotification>
            {
                new ()
                {
                    Id = 1,
                    IsRead = false
                },
                new ()
                {
                    Id = 2,
                    IsRead = false
                }
            };

            var command = new MarkNotificationsAsReadCommand
            {
                Ids = new int[] { 1, 2 }
            };

            _notificationRepository.GetAllAsync(Arg.Any<Guid>(), Arg.Any<IEnumerable<int>>()).Returns(notifications);

            await _commandHandler.Handle(command, default);

            notifications.ShouldAllBe(x => x.IsRead);

            await _notificationRepository.Received(1).UpdateAsync(Arg.Is<IEnumerable<AppNotification>>(x => x.Count() == 2));
        }
    }
}
