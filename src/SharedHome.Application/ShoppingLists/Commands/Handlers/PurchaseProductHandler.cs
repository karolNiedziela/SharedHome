using MediatR;
using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class PurchaseProductHandler : IRequestHandler<PurchaseProduct, Response<string>>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public PurchaseProductHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Response<string>> Handle(PurchaseProduct request, CancellationToken cancellationToken = default)
        {
            var shoppingList = await _shoppingListRepository.GetAsync(request.ShoppingListId);
            if (shoppingList is null)
            {
                throw new ShoppingListNotFoundException(request.ShoppingListId);
            }

            shoppingList.PurchaseProduct(request.ProductName, request.Price);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Response.Updated(nameof(ShoppingList));
        }
    }
}
