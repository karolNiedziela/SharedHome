using Microsoft.EntityFrameworkCore;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.Repositories;
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
            await _context.SaveChangesAsync();
        }

        private static List<ShoppingList> GetShoppingLists()
        {
            var firstShoppingList = ShoppingList.Create("Lidl", InitializerConstants.CharlesUserId);
            firstShoppingList.AddProducts(GetProducts());

            var secondShoppingList = ShoppingList.Create("Biedronka", InitializerConstants.CharlesUserId);
            secondShoppingList.AddProducts(GetProducts());

            var thirdShoppingList = ShoppingList.Create("Rossman", InitializerConstants.FrancUserId);
            thirdShoppingList.AddProducts(GetProducts());

            var fourthShoppingList = ShoppingList.Create("Tesco", InitializerConstants.FrancUserId);
            fourthShoppingList.AddProducts(GetProducts());


            return new List<ShoppingList> { firstShoppingList, secondShoppingList, thirdShoppingList, fourthShoppingList };
        }


        private static List<ShoppingListProduct> GetProducts()
        => new()
        {
            new ShoppingListProduct("Product1", 2),
            new ShoppingListProduct("Product2", 5, new Money(25m, "zł"), new NetContent("100", NetContentType.g), true),
            new ShoppingListProduct("Product3", 1),
            new ShoppingListProduct("Product4", 1),
        };
    }
}
