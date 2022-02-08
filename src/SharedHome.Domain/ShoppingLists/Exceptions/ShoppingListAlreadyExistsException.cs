using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListAlreadyExistsException : SharedHomeException
    {
        public string Name { get; }

        public ShoppingListAlreadyExistsException(string name) : base($"Shopping list with name '{name}' already exists.")
        {
            Name = name;
        }

    }
}
