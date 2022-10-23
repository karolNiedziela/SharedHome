using MediatR;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.CancelPurchaseOfProduct
{
    public class CancelPurchaseOfProductHandler : ICommandHandler<CancelPurchaseOfProductCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public CancelPurchaseOfProductHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(CancelPurchaseOfProductCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);    

            shoppingList.CancelPurchaseOfProduct(request.ProductName);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
