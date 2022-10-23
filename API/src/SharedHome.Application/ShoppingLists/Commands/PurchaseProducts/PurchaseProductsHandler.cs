using MediatR;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.PurchaseProducts
{
    public class PurchaseProductsHandler : ICommandHandler<PurchaseProductsCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public PurchaseProductsHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(PurchaseProductsCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            var priceByProductNames = request.PriceByProductNames.ToDictionary(x => x.Key,
                x => new Money(x.Value.Price, x.Value.Currency));

            shoppingList.PurchaseProducts(priceByProductNames);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
