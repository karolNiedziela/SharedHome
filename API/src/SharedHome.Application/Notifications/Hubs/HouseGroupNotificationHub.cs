using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.HouseGroups.Repositories;
using System.Collections.Concurrent;

namespace SharedHome.Application.Notifications.Hubs
{
    [Authorize]
    public class HouseGroupNotificationHub : Hub<IHouseGroupNotificationHubClient>
    {
        public static readonly ConcurrentDictionary<Guid, string> GroupNames = new();
        public static readonly ConcurrentDictionary<Guid, string> UserToConnection = new();

        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IHouseGroupRepository _houseGroupRepository;

        public HouseGroupNotificationHub(IHouseGroupReadService houseGroupReadService, IHouseGroupRepository houseGroupRepository)
        {
            _houseGroupReadService = houseGroupReadService;
            _houseGroupRepository = houseGroupRepository;
        }

        public string GetConnectionId() => Context.ConnectionId;

        public static string GetConnectionId(Guid personId)
        {
            if (!UserToConnection.TryGetValue(personId, out var connectionId))
            {
                return string.Empty;
            }

            return connectionId!;
        }

        public override async Task OnConnectedAsync()
        {
            var groupName = await GetGroupNameAsync();
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            GroupNames.TryAdd(new Guid(Context.UserIdentifier!), groupName);
            UserToConnection.TryAdd(new Guid(Context.UserIdentifier!), GetConnectionId());
            await Groups.AddToGroupAsync(GetConnectionId(), groupName);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var groupName = await GetGroupNameAsync();
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            GroupNames.TryRemove(new Guid(Context.UserIdentifier!), out _);
            UserToConnection.TryRemove(new Guid(Context.UserIdentifier!), out _);
            await Groups.RemoveFromGroupAsync(GetConnectionId(), groupName);
            await base.OnDisconnectedAsync(exception);
        }

        private async Task<string?> GetGroupNameAsync()
        {
            if (!await _houseGroupReadService.IsPersonInHouseGroup(new Guid(Context.UserIdentifier!)))
            {
                return string.Empty;
            }

            var houseGroup = await _houseGroupRepository.GetAsync(new Guid(Context.UserIdentifier!));

            return $"{houseGroup!.Id}-{houseGroup.Name.Value}";
        }
    }
}
