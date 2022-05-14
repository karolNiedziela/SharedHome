using MediatR;
using SharedHome.Application.Services;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class AddShoppingListProductHandler : ICommandHandler<AddShoppingListProduct, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IHouseGroupService _houseGroupService;

        public AddShoppingListProductHandler(IShoppingListRepository shoppingListRepository, IHouseGroupService houseGroupService)
        {
            _shoppingListRepository = shoppingListRepository;
            _houseGroupService = houseGroupService;
        }

        public async Task<Unit> Handle(AddShoppingListProduct request, CancellationToken cancellationToken)
        {
            var shoppingList = await _houseGroupService.IsPersonInHouseGroup(request.PersonId!) ?
                 await _houseGroupService.GetShoppingListAsync(request.ShoppingListId, request.PersonId!) :
                 await _shoppingListRepository.GetOrThrowAsync(request.ShoppingListId, request.PersonId!);

            var shoppingListProduct = new ShoppingListProduct(request.ProductName, request.Quantity);
            shoppingList.AddProduct(shoppingListProduct);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
