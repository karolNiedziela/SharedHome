using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.ShoppingLists.Commands.UpdateShoppingListProduct
{
    public class UpdateShoppingListProductCommand : AuthorizeRequest, ICommand<Unit>
    {
        public int ShoppingListId { get; set; }

        public string PreviousName { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Quantity { get; set; }

        public string? NetContent { get; set; }

        public int? NetContentType { get; set; } = default!;
    }
}
