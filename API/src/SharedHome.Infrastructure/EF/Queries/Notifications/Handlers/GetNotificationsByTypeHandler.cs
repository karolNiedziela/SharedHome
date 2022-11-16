using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Notifications.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Extensions;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;
using MediatR;
using SharedHome.Application.Common.Queries;

namespace SharedHome.Infrastructure.EF.Queries.Notifications.Handlers
{
    public class GetNotificationsByTypeHandler : IRequestHandler<GetNotificationsByType, Paged<AppNotificationDto>>
    {
        private readonly IMapper _mapper;
        private readonly DbSet<AppNotification> _notifications;

        public GetNotificationsByTypeHandler(IMapper mapper, WriteSharedHomeDbContext context)
        {
            _mapper = mapper;
            _notifications = context.Notifications;
        }

        public async Task<Paged<AppNotificationDto>> Handle(GetNotificationsByType request, CancellationToken cancellationToken)
        {
            var query = _notifications
                .Include(x => x.Fields)
                .Where(x => x.PersonId == request.PersonId)
                .AsQueryable();

            if (request.TargetType is not null && request.TargetType != (int)TargetType.All)
            {
                query = query.Where(x => x.Fields.Any(x => x.Value == Enum.GetName(typeof(TargetType), request.TargetType)));
            }

            if (request.NotificationType is not null && request.NotificationType != (int)NotificationType.All)
            {
                query = query.Where(x => (int?)x.Type == request.NotificationType.Value);
            }

            return await query.OrderByDescending(x => x.CreatedAt)
                .Select(x => _mapper.Map<AppNotificationDto>(x))
                .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
