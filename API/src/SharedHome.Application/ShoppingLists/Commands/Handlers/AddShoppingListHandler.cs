using MediatR;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class AddShoppingListHandler : ICommandHandler<AddShoppingList, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        //private readonly IMessageBroker _messageBroker;

        public AddShoppingListHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
            //_messageBroker = messageBroker;
        }

        public async Task<Unit> Handle(AddShoppingList request, CancellationToken cancellationToken)
        {
            var shoppingList = ShoppingList.Create(request.Name, request.PersonId!);

            shoppingList = await _shoppingListRepository.AddAsync(shoppingList);
            //await _messageBroker.PublishAsync(new ShoppingListCreated(shoppingList.Name), cancellationToken);

            return Unit.Value;
        }
    }
}
