using MediatR;
using SharedHome.Application.Common.Events;
using SharedHome.Application.ShoppingLists.Events;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingList
{
    public class AddShoppingListEventHandler : INotificationHandler<DomainEventNotification<ShoppingListCreated>>
    {        
        public Task Handle(DomainEventNotification<ShoppingListCreated> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;


            return Task.CompletedTask;
        }
    }
}
