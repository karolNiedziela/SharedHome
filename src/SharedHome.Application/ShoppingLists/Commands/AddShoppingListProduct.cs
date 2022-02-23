using SharedHome.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record AddShoppingListProduct(int ShoppingListId, string Name, int Quantity) : ICommand;
}
