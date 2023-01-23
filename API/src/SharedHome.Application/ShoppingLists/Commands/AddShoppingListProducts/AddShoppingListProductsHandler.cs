using MapsterMapper;
using MediatR;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Domain.ShoppingLists.ValueObjects;

using SharedHome.Shared.Helpers;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingListProducts
{
    public class AddShoppingListProductsHandler : IRequestHandler<AddShoppingListProductsCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public AddShoppingListProductsHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(AddShoppingListProductsCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            var products = new List<ShoppingListProduct>();

            foreach (var product in request.Products)
            {
               
                NetContentType? netContentType = product.NetContent is not null && product.NetContent!.NetContentType.HasValue ? EnumHelper.ToEnumByIntOrThrow<NetContentType>(product.NetContent!.NetContentType!.Value) : null;
                var netContent = new NetContent(product.NetContent?.NetContent, netContentType);

                products.Add(ShoppingListProduct.Create(product.Name, product.Quantity, null, netContent, false, Guid.NewGuid()));
            }

            shoppingList.AddProducts(products);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
