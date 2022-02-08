using SharedHome.Domain.ShoppingLists.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public record ShoppingListName
    {
        public string Name { get; }

        public ShoppingListName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new EmptyShoppingListNameException();
            }

            Name = name;
        }

        public static implicit operator string(ShoppingListName name) => name.Name;

        public static implicit operator ShoppingListName(string name) => new(name);
    }
}
