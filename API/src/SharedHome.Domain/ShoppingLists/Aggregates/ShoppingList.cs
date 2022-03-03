using SharedHome.Domain.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedHome.Domain.ShoppingLists.Events;

namespace SharedHome.Domain.ShoppingLists.Aggregates
{
    public class ShoppingList : Entity, IAggregateRoot
    {
        private readonly LinkedList<ShoppingListProduct> _products = new();

        public int Id { get; private set; }

        public Guid PersonId { get; private set; }

        public ShoppingListName Name { get; private set; } = default!;

        // Indicator to mark, that list is closed
        public bool IsDone { get; private set; } = false;

        public IEnumerable<ShoppingListProduct> Products => _products;

        private ShoppingList()
        {

        }

        private ShoppingList(ShoppingListName name, Guid personId, bool isDone = false)
        {
            Name = name;
            PersonId = personId;
            IsDone = isDone;
        }

        public static ShoppingList Create(ShoppingListName name, Guid personId, bool isDone = false)
            => new (name, personId, isDone);

        public void ChangeName(ShoppingListName shoppingListName)
        {
            Name = shoppingListName;
        }

        public void AddProduct(ShoppingListProduct product)
        {
            IsAlreadyDone();

            if (_products.Any(p => p.Name == product.Name))
            {
                throw new ShoppingListProductAlreadyExistsException(Name, product.Name);
            }

            _products.AddLast(product);

            AddEvent(new ShoppingListProductAdded(Id, product.Name));
        }

        public void AddProducts(IEnumerable<ShoppingListProduct> products)
        {
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

            AddEvent(new ShoppingListProductRemoved(Id, productName));
        }

        public void PurchaseProduct(string productName, ProductPrice price)
        {
            IsAlreadyDone();

            var product = GetProduct(productName);
            if (product.IsBought)
            {
                throw new ShoppingListProductIsAlreadyBoughtException(productName);
            }

            product.Update(new ShoppingListProduct(product.Name, product.Quantity, price, true));

            AddEvent(new ShoppingListProductPurchased(Id, productName, price));
        }

        public void ChangePriceOfProduct(string productName, ProductPrice price)
        {
            IsAlreadyDone();

            var product = GetProduct(productName);
            if (!product.IsBought)
            {
                throw new ShoppingListProductWasNotBoughtException(productName);
            }

            product.Update(new ShoppingListProduct(product.Name, product.Quantity, price, product.IsBought));

            AddEvent(new ShoppingListProductPriceChanged(Id, productName, price));
        }

        public void CancelPurchaseOfProduct(string productName)
        {
            IsAlreadyDone();

            var product = GetProduct(productName);
            if (product.IsBought == false)
            {
                throw new ShoppingListProductWasNotBoughtException(productName);
            }
            product.Update(new ShoppingListProduct(product.Name, product.Quantity, null, false));

            AddEvent(new ShoppingListProductPurchaseCanceled(Id, productName));
        }

        // Sums all product prices for products which are bought
        public decimal SumProductPrices()
            => _products.Where(product => product.IsBought).Aggregate((decimal)0, (count, product) => count + (product.Quantity * (decimal)product.Price!));

        public void MakeListDone()
        {
            IsAlreadyDone();
            IsDone = true;

            AddEvent(new ShoppingListDone(Id));
        }

        public void UndoListDone()
        {
            IsDone = false;

            AddEvent(new ShoppingListUndone(Id));
        }

        private ShoppingListProduct GetProduct(string productName)
        {
            var product = _products.SingleOrDefault(p => p.Name == productName);
            if (product is null)
            {
                throw new ShoppingListProductNotFoundException(productName);
            }

            return product;
        }

        // When list is done most of the operations are blocked
        private void IsAlreadyDone()
        {
            if (IsDone)
            {
                throw new ShoppingListAlreadyDoneException();
            }
        }
    }
}
