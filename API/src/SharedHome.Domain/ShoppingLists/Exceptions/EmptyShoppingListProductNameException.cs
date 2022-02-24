using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class EmptyShoppingListProductNameException : SharedHomeException
    {
        public EmptyShoppingListProductNameException() : base("Shopping list product cannot be empty.")
        {
        }
    }
}
