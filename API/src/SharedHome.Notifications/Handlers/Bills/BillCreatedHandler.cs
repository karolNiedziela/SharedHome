using MediatR;
using SharedHome.Application.Bills.Events;
using SharedHome.Application.Common.Events;
using SharedHome.Application.ReadServices;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Repositories;
using SharedHome.Notifications.Services;

namespace SharedHome.Notifications.Handlers.Bills
{
    public class BillCreatedHandler : INotificationHandler<DomainEventNotification<BillCreated>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;

        public BillCreatedHandler(INotificationRepository notificationRepository, IHouseGroupReadService houseGroupReadService, IAppNotificationService appNotificationService)
        {
            _notificationRepository = notificationRepository;
            _houseGroupReadService = houseGroupReadService;
            _appNotificationService = appNotificationService;
        }

        public async Task Handle(DomainEventNotification<BillCreated> notification, CancellationToken cancellationToken)
        {
            var billCreated = notification.DomainEvent;

            if (!await _houseGroupReadService.IsPersonInHouseGroup(billCreated.Creator.PersonId))
            {
                return;
            }

            var personIds = await _houseGroupReadService.GetMemberPersonIdsExcludingCreator(billCreated.Creator.PersonId);

            foreach (var personId in personIds)
            {
                var appNotification = new AppNotification(billCreated.Creator.PersonId, nameof(BillCreated), TargetType.Bill, OperationType.Create);

                await _notificationRepository.AddAsync(appNotification);

                await _appNotificationService.BroadcastNotificationAsync(appNotification, personId, billCreated.Creator.PersonId, billCreated.ServiceProviderName);
            }
        }
    }
}
