using MediatR;
using SharedHome.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.ShoppingLists.Commands
{
    public record PurchaseProduct(int ShoppingListId, string ProductName, decimal Price) : IRequest<Response<string>>;
}
