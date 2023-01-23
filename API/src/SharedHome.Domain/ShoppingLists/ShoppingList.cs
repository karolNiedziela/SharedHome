using SharedHome.Domain.Primivites;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Domain.ShoppingLists.Events;
using SharedHome.Domain.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.ValueObjects;

namespace SharedHome.Domain.ShoppingLists
{
    public sealed class ShoppingList : AggregateRoot<ShoppingListId>
    {
        private readonly List<ShoppingListProduct> _products = new();

        public ShoppingListName Name { get; private set; } = default!;

        public ShoppingListStatus Status { get; private set; }

        public IReadOnlyList<ShoppingListProduct> Products => _products.AsReadOnly();

        public PersonId PersonId { get; private set; } = default!;

        public DateTime CreationDate { get; private set; }



        private ShoppingList()
        {

        }

        private ShoppingList(ShoppingListId id, ShoppingListName name, PersonId personId, DateTime creationDate, IEnumerable<ShoppingListProduct>? products = null)
        {
            Id = id;
            Name = name;
            PersonId = personId;
            Status = ShoppingListStatus.ToDo;
            CreationDate = creationDate;

            AddProducts(products);

            AddEvent(new ShoppingListCreated(id, name, personId));
        }

        public static ShoppingList Create(ShoppingListId id, ShoppingListName name, PersonId personId, DateTime creationDate, IEnumerable<ShoppingListProduct>? products = null)
            => new (id, name, personId, creationDate, products);

        public void ChangeName(ShoppingListName shoppingListName)
        {
            Name = shoppingListName;
        }

        public void AddProduct(ShoppingListProduct product)
        {
            IsAlreadyDone();

            if (_products.Any(p => p.Name == product.Name))
            {
                throw new ShoppingListProductAlreadyExistsException(product.Name, Id);
            }

            _products.Add(product);
        }

        public void AddProducts(IEnumerable<ShoppingListProduct>? products)
        {
            if (products is null || !products.Any()) return; 

            IsAlreadyDone();

            foreach (var product in products)
            {
                AddProduct(product);
            }
        }

        public void RemoveProduct(string productName)
        {
            IsAlreadyDone();

            var product = GetProduct(productName);
            _products.Remove(product);
        }

        public void RemoveProducts(IEnumerable<string> productNames)
        {
            IsAlreadyDone();

            foreach (var productName in productNames)
            {
                RemoveProduct(productName);
            }
        }

        public void PurchaseProduct(string productName, Money price)
        {
            IsAlreadyDone();

            var product = GetProduct(productName);
            if (product.IsBought)
            {
                throw new ShoppingListProductIsAlreadyBoughtException(productName);
            }

            product.PurchaseProduct(price);
        }

        public void PurchaseProducts(Dictionary<string, Money> priceByProductNames)
        {
            IsAlreadyDone();

            foreach (var pricebyProductName in priceByProductNames)
            {
               PurchaseProduct(pricebyProductName.Key, pricebyProductName.Value);
            }
        }

        public void ChangePriceOfProduct(string productName, Money? price)
        {
            IsAlreadyDone();

            var product = GetProduct(productName);
            if (!product.IsBought)
            {
                throw new ShoppingListProductWasNotBoughtException(productName);
            }

            product.ChangePriceOfProduct(price);
        }

        public void CancelPurchaseOfProduct(string productName)
        {
            IsAlreadyDone();

            var product = GetProduct(productName);
            if (product.IsBought == false)
            {
                throw new ShoppingListProductWasNotBoughtException(productName);
            }

            product.CancelPurchaseOfProduct();
        }

        public void MarkAsDone()
        {
            IsAlreadyDone();
            Status = ShoppingListStatus.Done;          
        }

        public void MarkAsToDo() => Status = ShoppingListStatus.ToDo;

        public ShoppingListProduct GetProduct(string productName)
        {
            var product = _products.SingleOrDefault(p => p.Name == productName);
            if (product is null)
            {
                throw new ShoppingListProductNotFoundException(productName);
            }

            return product;
        }

        private void IsAlreadyDone()
        {            
            if (Status == ShoppingListStatus.Done)
            {
                throw new ShoppingListAlreadyDoneException();
            }
        }
    }
}
