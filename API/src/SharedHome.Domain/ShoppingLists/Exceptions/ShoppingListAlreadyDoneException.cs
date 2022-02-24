using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListAlreadyDoneException : SharedHomeException
    {
        public ShoppingListAlreadyDoneException() : base("Shopping list is already done.")
        {
        }
    }
}
