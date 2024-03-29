﻿using MediatR;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;


namespace SharedHome.Application.ShoppingLists.Commands.ChangePriceOfProduct
{
    public class ChangePriceOfProductHandler : IRequestHandler<ChangePriceOfProductCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public ChangePriceOfProductHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(ChangePriceOfProductCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            var money = request.Price == null ? null : new Money(request.Price.Price, request.Price.Currency);

            shoppingList.ChangePriceOfProduct(request.ProductName, money);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
