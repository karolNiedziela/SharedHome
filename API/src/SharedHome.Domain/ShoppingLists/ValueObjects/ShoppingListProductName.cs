using SharedHome.Domain.ShoppingLists.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public record ShoppingListProductName
    {
        public string Value { get; }

        public ShoppingListProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyShoppingListProductNameException();
            }

            Value = value;
        }

        public static implicit operator string(ShoppingListProductName name) => name.Value;

        public static implicit operator ShoppingListProductName(string value) => new(value);
    }
}
