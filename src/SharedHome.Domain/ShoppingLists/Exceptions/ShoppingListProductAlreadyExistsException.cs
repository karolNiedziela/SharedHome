using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductAlreadyExistsException : SharedHomeException
    {
        public string ShoppingListName { get; }

        public string ProductName { get; }

        public ShoppingListProductAlreadyExistsException(string shoppingListName, string productName) 
            : base($"Shopping list '{shoppingListName} already has product '{productName}'")
        {
            ShoppingListName = shoppingListName;
            ProductName = productName;
        }
    }
}
