using MediatR;
using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class AddShoppingListProductHandler : ICommandHandler<AddShoppingListProduct, Unit>
    {
        private IShoppingListRepository _shoppingListRepository;

        public AddShoppingListProductHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Unit> Handle(AddShoppingListProduct request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListRepository.GetAsync(request.ShoppingListId);
            if (shoppingList is null)
            {
                throw new ShoppingListNotFoundException(request.ShoppingListId);
            }

            var shoppingListProduct = new ShoppingListProduct(request.Name, request.Quantity);
            shoppingList.AddProduct(shoppingListProduct);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
