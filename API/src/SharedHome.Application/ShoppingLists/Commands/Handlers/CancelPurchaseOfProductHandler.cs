using MediatR;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class CancelPurchaseOfProductHandler : ICommandHandler<CancelPurchaseOfProduct, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public CancelPurchaseOfProductHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Unit> Handle(CancelPurchaseOfProduct request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListRepository.GetOrThrowAsync(request.ShoppingListId);

            shoppingList.CancelPurchaseOfProduct(request.ProductName);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
