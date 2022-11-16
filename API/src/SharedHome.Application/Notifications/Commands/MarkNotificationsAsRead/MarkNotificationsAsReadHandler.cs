using MediatR;
using SharedHome.Notifications.Repositories;


namespace SharedHome.Application.Notifications.Commands.MarkNotificationsAsRead
{
    public class MarkNotificationsAsReadHandler : IRequestHandler<MarkNotificationsAsReadCommand, Unit>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkNotificationsAsReadHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(MarkNotificationsAsReadCommand request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetAllAsync(request.PersonId, false);

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _notificationRepository.UpdateAsync(notifications);

            return Unit.Value;
        }
    }
}
