using MediatR;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Domain.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.ShoppingLists.Commands.UpdateShoppingListProduct
{
    public class UpdateShoppingListProductCommandHandler : IRequestHandler<UpdateShoppingListProductCommand, Unit>
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
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            NetContentType? netContentType = request.NetContent is not null && request.NetContent!.NetContentType.HasValue ?
                EnumHelper.ToEnumByIntOrThrow<NetContentType>(request.NetContent!.NetContentType!.Value) 
                : null;
            var netContent = new NetContent(request.NetContent?.NetContent, netContentType);

            var shoppingListProduct = ShoppingListProduct.Create(request.NewProductName,
                                                 request.Quantity,
                                                 netContent: netContent,
                                                 isBought: request.IsBought);

            shoppingList.UpdateShoppingListProduct(request.CurrentProductName, shoppingListProduct);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
