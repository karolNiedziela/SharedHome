using MediatR;
using SharedHome.Domain.Bills.Events;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Application.Bills.Events
{
    public class BillCreatedEventHandler : INotificationHandler<BillCreated>
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IAppNotificationService _appNotificationService;

        public BillCreatedEventHandler(IHouseGroupRepository houseGroupRepository, IAppNotificationService appNotificationService)
        {
            _houseGroupRepository = houseGroupRepository;
            _appNotificationService = appNotificationService;
        }       

        public async Task Handle(BillCreated notification, CancellationToken cancellationToken)
        {
            if (!await _houseGroupRepository.IsPersonInHouseGroupAsync(notification.PersonId))
            {
                return;
            }

            var personIds = await _houseGroupRepository.GetMemberPersonIdsExcludingCreatorAsync(notification.PersonId);

            foreach (var personId in personIds)
            {
                var notificationFields = new List<AppNotificationField>()
                {
                    new AppNotificationField(AppNotificationFieldType.Name, notification.ServiceProviderName),
                    new AppNotificationField(AppNotificationFieldType.Target, TargetType.Bill.ToString()),
                    new AppNotificationField(AppNotificationFieldType.Operation, OperationType.Create.ToString()),
                    new AppNotificationField(AppNotificationFieldType.DateOfPayment, notification.DateOfPayment.ToShortDateString())
                };

                var appNotification = new AppNotification(personId, nameof(BillCreated), notificationFields, notification.PersonId);

                await _appNotificationService.AddAsync(appNotification);

                await _appNotificationService.BroadcastNotificationAsync(appNotification, personId, notification.PersonId);
            }
        }
    }
}
