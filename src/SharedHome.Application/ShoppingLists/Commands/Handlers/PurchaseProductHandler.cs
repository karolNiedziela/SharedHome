using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class PurchaseProductHandler : ICommandHandler<PurchaseProduct>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public PurchaseProductHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task HandleAsync(PurchaseProduct command)
        {
            var shoppingList = await _shoppingListRepository.GetAsync(command.ShoppingListId);
            if (shoppingList is null)
            {
                throw new ShoppingListNotFoundException(command.ShoppingListId);
            }

            shoppingList.PurchaseProduct(command.ProductName, command.Price);

            await _shoppingListRepository.UpdateAsync(shoppingList);
        } 
    }
}
