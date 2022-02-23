using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class AddShoppingListProductHandler : ICommandHandler<AddShoppingListProduct>
    {
        private IShoppingListRepository _shoppingListRepository;

        public AddShoppingListProductHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task HandleAsync(AddShoppingListProduct command)
        {
            var shoppingList = await _shoppingListRepository.GetAsync(command.ShoppingListId);
            if (shoppingList is null)
            {
                throw new ShoppingListNotFoundException(command.ShoppingListId);
            }

            var shoppingListProduct = new ShoppingListProduct(command.Name, command.Quantity);
            shoppingList.AddProduct(shoppingListProduct);

            await _shoppingListRepository.UpdateAsync(shoppingList);
        }
    }
}
