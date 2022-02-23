using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.EF.Repositories
{
    internal sealed class ShoppingListRepository : IShoppingListRepository
    {
        public Task<ShoppingList> AddAsync(ShoppingList shoppingList)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ShoppingList shoppingList)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingList> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ShoppingList shoppingList)
        {
            throw new NotImplementedException();
        }
    }
}
