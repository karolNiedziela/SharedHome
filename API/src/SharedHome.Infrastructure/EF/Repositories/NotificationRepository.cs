using Microsoft.EntityFrameworkCore;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Repositories;

namespace SharedHome.Infrastructure.EF.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly WriteSharedHomeDbContext _context;

        public NotificationRepository(WriteSharedHomeDbContext context)
        {
            _context = context;
        }      

        public async Task<IEnumerable<AppNotification>> GetAll(NotificationType? notificationType, TargetType? targetType)
        {
            var query = _context.Notifications.AsQueryable();

            if (notificationType is null)
            {
                query = query.Where(x => x.Type == notificationType);
            }

            if (targetType is not null)
            {
                query = query.Where(x => x.Target == targetType);
            }

            query.Where(x => !x.IsRead);

            return await query.ToListAsync();
        }

        public async Task AddAsync(AppNotification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
    }
}
