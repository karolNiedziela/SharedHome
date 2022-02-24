using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.ShoppingLists.Exceptions
{
    public class ShoppingListNotFoundException : SharedHomeException
    {
        public int Id { get; }

        public ShoppingListNotFoundException(int id) : base($"Shopping list with id '{id}' was not found.")
        {
            Id = id;
        }

    }
}
