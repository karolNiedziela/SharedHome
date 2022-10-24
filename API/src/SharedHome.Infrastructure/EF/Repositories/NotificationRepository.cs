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

        public async Task<IEnumerable<AppNotification>> GetAllAsync(Guid personId, NotificationType? notificationType = null, TargetType? targetType = null)
        {
            var query = _context.Notifications.Include(x => x.Fields).Where(x => x.PersonId == personId).AsQueryable();

            if (notificationType is not null)
            {
                query = query.Where(x => x.Type == notificationType);
            }

            query.Where(x => !x.IsRead);

            return await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task AddAsync(AppNotification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
    }
}
