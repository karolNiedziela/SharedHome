﻿using SharedHome.Shared.Attributes;
using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class ShoppingListProductAlreadyExistsException : SharedHomeException
    {
        public override string ErrorCode => "ShoppingListProductAlreadyExists";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        [Order]
        public string ProductName { get; }

        public Guid ShoppingListId { get; }

        public ShoppingListProductAlreadyExistsException(string productName, Guid shoppingListId) 
            : base($"Shopping list product with name '{productName}' already added to shopping list with id '{shoppingListId}'.")
        {
            ProductName = productName;
            ShoppingListId = shoppingListId;
        }
    }
}
