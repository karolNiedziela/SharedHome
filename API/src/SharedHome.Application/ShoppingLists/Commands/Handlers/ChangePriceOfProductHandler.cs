using MediatR;
using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class ChangePriceOfProductHandler : ICommandHandler<ChangePriceOfProduct, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public ChangePriceOfProductHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Unit> Handle(ChangePriceOfProduct request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListRepository.GetOrThrowAsync(request.ShoppingListId);

            shoppingList.ChangePriceOfProduct(request.ProductName, request.Price);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
