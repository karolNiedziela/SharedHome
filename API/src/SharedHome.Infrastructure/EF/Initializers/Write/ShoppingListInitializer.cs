﻿using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Initializers.Write
{
    public class ShoppingListInitializer : IDataInitializer
    {
        private readonly WriteSharedHomeDbContext _context;

        public ShoppingListInitializer(WriteSharedHomeDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.ShoppingLists.AnyAsync()) return;

            var shoppingLists = GetShoppingLists();

            await _context.ShoppingLists.AddRangeAsync(shoppingLists);

            shoppingLists.ForEach(shoppingList => shoppingList.ClearEvents());
            await _context.SaveChangesAsync();
        }

        private static List<ShoppingList> GetShoppingLists()
        {
            var currentDate = DateTime.UtcNow;
            var firstShoppingList = ShoppingList.Create(Guid.NewGuid(), "Lidl", InitializerConstants.CharlesUserId, currentDate);
            firstShoppingList.CreatedByFullName = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            firstShoppingList.CreatedBy = InitializerConstants.CharlesUserId;
            firstShoppingList.AddProducts(GetProducts());

            var secondShoppingList = ShoppingList.Create(Guid.NewGuid(), "Biedronka", InitializerConstants.CharlesUserId, currentDate);
            secondShoppingList.CreatedByFullName = $"{InitializerConstants.CharlesFirstName} {InitializerConstants.CharlesLastName}";
            secondShoppingList.CreatedBy = InitializerConstants.CharlesUserId;
            secondShoppingList.AddProducts(GetProducts());

            var thirdShoppingList = ShoppingList.Create(Guid.NewGuid(), "Rossman", InitializerConstants.FrancUserId, currentDate);
            thirdShoppingList.CreatedByFullName = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            thirdShoppingList.CreatedBy = InitializerConstants.FrancUserId;
            thirdShoppingList.AddProducts(GetProducts());

            var fourthShoppingList = ShoppingList.Create(Guid.NewGuid(), "Tesco", InitializerConstants.FrancUserId, currentDate);
            fourthShoppingList.CreatedByFullName = $"{InitializerConstants.FrancFirstName} {InitializerConstants.FrancLastName}";
            fourthShoppingList.CreatedBy = InitializerConstants.FrancUserId;
            fourthShoppingList.AddProducts(GetProducts());


            return new List<ShoppingList> { firstShoppingList, secondShoppingList, thirdShoppingList, fourthShoppingList };
        }


        private static List<ShoppingListProduct> GetProducts()
        => new()
        {
            ShoppingListProduct.Create("Product1", 2, id: Guid.NewGuid()),
            ShoppingListProduct.Create("Product2", 5, new Money(25m, "zł"), new NetContent("100", NetContentType.g), true, id: Guid.NewGuid()),
            ShoppingListProduct.Create("Product3", 1, id: Guid.NewGuid()),
            ShoppingListProduct.Create("Product4", 1, id: Guid.NewGuid()),
        };
    }
}
