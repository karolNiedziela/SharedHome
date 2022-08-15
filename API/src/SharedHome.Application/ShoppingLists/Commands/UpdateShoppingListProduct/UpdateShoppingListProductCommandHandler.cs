using MediatR;
using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.ShoppingLists.Commands.UpdateShoppingListProduct
{
    public class UpdateShoppingListProductCommandHandler : ICommandHandler<UpdateShoppingListProductCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public UpdateShoppingListProductCommandHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }


        public async Task<Unit> Handle(UpdateShoppingListProductCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId!);

            NetContentType? netContentType = request.NetContentType.HasValue ? 
                EnumHelper.ToEnumByIntOrThrow<NetContentType>(request.NetContentType.Value) :
                null;

            var shoppingListProduct = new ShoppingListProduct(request.NewProductName,
                                                              request.Quantity,
                                                              netContent: new NetContent(request.NetContent, netContentType));

            shoppingList.UpdateProduct(shoppingListProduct, request.CurrentProductName);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
