using SharedHome.Domain.ShoppingLists.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Repositories
{
    public interface IShoppingListRepository
    {
        Task<ShoppingList?> GetAsync(int id);

        Task<ShoppingList> AddAsync(ShoppingList shoppingList);

        Task UpdateAsync(ShoppingList shoppingList);

        Task DeleteAsync(ShoppingList shoppingList);
    }
}
