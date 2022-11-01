using MediatR;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Domain.ShoppingLists.Exceptions;
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
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            var netContent = request.NetContent == null ? null 
                : new NetContent(request.NetContent.NetContent, 
                    request.NetContent.NetContentType.HasValue 
                        ? EnumHelper.ToEnumByIntOrThrow<NetContentType>(request.NetContent.NetContentType.Value) 
                        : null);
           

            if (request.NewProductName != request.CurrentProductName && shoppingList.Products.Any(p => p.Name == request.NewProductName))
            {
                throw new ShoppingListProductAlreadyExistsException(request.NewProductName, request.ShoppingListId);
            }

            var product = shoppingList.GetProduct(request.CurrentProductName);           
            var shoppingListProduct = ShoppingListProduct.Create(request.NewProductName,
                                                 request.Quantity,
                                                 product.Price,
                                                 netContent: netContent,
                                                 isBought: request.IsBought, 
                                                 product.Id);

            product.Update(shoppingListProduct);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
