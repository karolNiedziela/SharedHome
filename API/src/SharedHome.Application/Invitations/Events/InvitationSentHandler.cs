using MediatR;
using SharedHome.Domain.Invitations.Events;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Application.Invitations.Events
{
    public class InvitationSentHandler : INotificationHandler<InvitationSent>
    {
        private readonly IAppNotificationService _appNotificationService;
        private readonly IPersonRepository _personRepository;

        public InvitationSentHandler(IAppNotificationService appNotificationService, IPersonRepository personRepository)
        {
            _appNotificationService = appNotificationService;
            _personRepository = personRepository;
        }

        public async Task Handle(InvitationSent notification, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetAsync(notification.RequestedByPersonId);
            var fullName = person is null ? string.Empty : $"{person.FirstName.Value}  {person.LastName.Value}";

            var notificationFields = new List<AppNotificationField>()
            {
                new AppNotificationField(AppNotificationFieldType.Target, TargetType.Invitation.ToString()),
                new AppNotificationField(AppNotificationFieldType.Operation, OperationType.Send.ToString()),
                new AppNotificationField(AppNotificationFieldType.SentBy, fullName),
            };
            var appNotification = new AppNotification(notification.RequestedToPersonId, nameof(InvitationSent), notificationFields);
            await _appNotificationService.AddAsync(appNotification);

            await _appNotificationService.BroadcastNotificationAsync(appNotification, notification.RequestedToPersonId);
        }
    }
}
