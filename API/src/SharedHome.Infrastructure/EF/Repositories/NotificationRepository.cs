﻿using Microsoft.EntityFrameworkCore;
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
            var query = _context.Notifications.AsNoTracking().Include(x => x.Fields).Where(x => x.PersonId == personId).AsQueryable();

            if (notificationType is not null)
            {
                query = query.Where(x => x.Type == notificationType);
            }

            query.Where(x => !x.IsRead);

            return await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<AppNotification>> GetAllAsync(Guid personId, bool isRead)
            => await _context.Notifications
            .AsNoTracking()
            .Where(x => x.PersonId == personId && x.IsRead == isRead)
            .ToListAsync();

        public async Task AddAsync(AppNotification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<AppNotification> notification)
        {
            _context.Notifications.UpdateRange(notification);
            await _context.SaveChangesAsync();
        }
    }
}
