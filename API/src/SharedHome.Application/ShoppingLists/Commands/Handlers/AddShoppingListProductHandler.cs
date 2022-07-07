using MediatR;
using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class AddShoppingListProductHandler : ICommandHandler<AddShoppingListProduct, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public AddShoppingListProductHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(AddShoppingListProduct request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId!);

            NetContentType? netContentType = request.NetContentType.HasValue ? EnumHelper.ToEnumByIntOrThrow<NetContentType>(request.NetContentType.Value) : null;
            NetContent? netContent = string.IsNullOrEmpty(request.NetContent) ? null : new NetContent(request.NetContent!, netContentType);

            var shoppingListProduct = new ShoppingListProduct(request.ProductName, request.Quantity, netContent: netContent);
            shoppingList.AddProduct(shoppingListProduct);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
