using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductIsAlreadyBoughtException : SharedHomeException
    {
        public string ProductName { get; }

        public ShoppingListProductIsAlreadyBoughtException(string productName)
            : base($"Shopping list product '{productName}' is already bought.")
        {
            ProductName = productName;
        }

    }
}
