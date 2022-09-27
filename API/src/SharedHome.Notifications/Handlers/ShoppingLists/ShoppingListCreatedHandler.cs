using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SharedHome.Application.Common.Events;
using SharedHome.Application.ReadServices;
using SharedHome.Application.ShoppingLists.Events;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Hubs;
using SharedHome.Notifications.Repositories;
using SharedHome.Notifications.Services;

namespace SharedHome.Notifications.Handlers.ShoppingLists
{
    internal class ShoppingListCreatedHandler : INotificationHandler<DomainEventNotification<ShoppingListCreated>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IMapper _mapper;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public ShoppingListCreatedHandler(INotificationRepository notificationRepository, IHouseGroupReadService houseGroupReadService, IHubContext<BroadcastHub, IHubClient> hubContext, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _houseGroupReadService = houseGroupReadService;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public async Task Handle(DomainEventNotification<ShoppingListCreated> notification, CancellationToken cancellationToken)
        {
            var shoppingListCreated = notification.DomainEvent;

            if (!await _houseGroupReadService.IsPersonInHouseGroup(shoppingListCreated.Creator.PersonId))
            {
                return;
            }

            var personIds = await _houseGroupReadService.GetMemberPersonIdsExcludingCreator(shoppingListCreated.Creator.PersonId);

            foreach (var personId in personIds)
            {
                var appNotification = new AppNotification(shoppingListCreated.Creator.PersonId, nameof(ShoppingListCreated), TargetType.ShoppingList, OperationType.Create);

                await _notificationRepository.AddAsync(appNotification);

                var notificationDto = _mapper.Map<AppNotificationDto>(appNotification);

                _hubContext.Clients.All.BroadcastNotification(notificationDto);
            }            


        }
    }
}
