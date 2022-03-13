using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class TooLongShoppingListNameException : SharedHomeException
    {
        public int MaximumLength { get; }

        public TooLongShoppingListNameException(int maximumLength) 
            : base($"Name can contain maximum {maximumLength} characters.")
        {
            MaximumLength = maximumLength;
        }

    }
}
