using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class AddShoppingListHandler : ICommandHandler<AddShoppingList>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        //private readonly IMessageBroker _messageBroker;

        public AddShoppingListHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
            //_messageBroker = messageBroker;
        }

        public async Task HandleAsync(AddShoppingList command)
        {
            var shoppingList = ShoppingList.Create(command.Name, command.PersonId);

            shoppingList = await _shoppingListRepository.AddAsync(shoppingList);
            //await _messageBroker.PublishAsync(new ShoppingListCreated(shoppingList.Name), cancellationToken);
        }
    }
}
