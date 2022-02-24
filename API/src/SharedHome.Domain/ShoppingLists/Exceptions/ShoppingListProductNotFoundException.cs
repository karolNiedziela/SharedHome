using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductNotFoundException : SharedHomeException
    {
        public string ProductName { get; }

        public ShoppingListProductNotFoundException(string productName) 
            : base($"Shopping list product '{productName}' was not found. ")
        {
            ProductName = productName;
        }
    }
}
