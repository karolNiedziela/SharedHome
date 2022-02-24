using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public record ShoppingListId
    {
        public Guid Value { get; }

        public ShoppingListId() : this(Guid.NewGuid())
        {

        }

        public ShoppingListId(Guid value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();

        public static implicit operator Guid(ShoppingListId id) => id.Value;

        public static implicit operator ShoppingListId(Guid id) => new(id);
    }
}
