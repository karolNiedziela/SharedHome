using MediatR;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Events;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Messaging;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class AddShoppingListHandler : IRequestHandler<AddShoppingList, Response<string>>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMessageBroker _messageBroker;

        public AddShoppingListHandler(IShoppingListRepository shoppingListRepository, IMessageBroker messageBroker)
        {
            _shoppingListRepository = shoppingListRepository;
            _messageBroker = messageBroker;
        }

        public async Task<Response<string>> Handle(AddShoppingList request, CancellationToken cancellationToken = default)
        {
            var shoppingList = ShoppingList.Create(request.Name, request.PersonId);

            await _shoppingListRepository.AddAsync(shoppingList);
            await _messageBroker.PublishAsync(new ShoppingListCreated(shoppingList.Name), cancellationToken);

            return Response.Added(nameof(ShoppingList));
        }
    }
}
