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
        private const int MaximumLength = 20;

        public string Name { get; }

        public ShoppingListName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new EmptyShoppingListNameException();
            }

            if (name.Length > MaximumLength)
            {
                throw new TooLongShoppingListNameException(MaximumLength);
            }

            Name = name;
        }

        public static implicit operator string(ShoppingListName name) => name.Name;

        public static implicit operator ShoppingListName(string name) => new(name);
    }
}
