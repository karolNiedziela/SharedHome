using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Notifications.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Extensions;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;
using SharedHome.Shared.Abstractions.Queries;

namespace SharedHome.Infrastructure.EF.Queries.Notifications.Handlers
{
    internal class GetNotificationsHandler : IQueryHandler<GetNotifications, Paged<AppNotificationDto>>
    {
        private readonly IMapper _mapper;
        private readonly DbSet<AppNotification> _notifications;

        public GetNotificationsHandler(IMapper mapper, WriteSharedHomeDbContext context)
        {
            _mapper = mapper;
            _notifications = context.Notifications;
        }

        public async Task<Paged<AppNotificationDto>> Handle(GetNotifications request, CancellationToken cancellationToken)
        {
            var notifications = await _notifications
                .Include(x => x.Fields)
                .Where(x => x.PersonId == request.PersonId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => _mapper.Map<AppNotificationDto>(x))
                .PaginateAsync(request.PageNumber, request.PageSize);

            var unReadNotifications = notifications.Items.Count(x => !x.IsRead);
            notifications.CustomTotalItems = unReadNotifications;

            return notifications;
        }
    }
}
