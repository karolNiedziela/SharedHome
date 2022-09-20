using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Exceptions;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Domain.ShoppingLists.Aggregates
{
    public class ShoppingList : Entity, IAggregateRoot
    {
        private readonly List<ShoppingListProduct> _products = new();

        public int Id { get; private set; }

        public ShoppingListName Name { get; private set; } = default!;

        // Indicator to mark, that list is closed
        public bool IsDone { get; private set; } = false;

        public IEnumerable<ShoppingListProduct> Products => _products;

        public string PersonId { get; private set; } = default!;

        private ShoppingList()
        {

        }

        private ShoppingList(ShoppingListName name, string personId, bool isDone = false, IEnumerable<ShoppingListProduct>? products = null)
        {
            Name = name;
            PersonId = personId;
            IsDone = isDone;

            AddProducts(products);
        }

        public static ShoppingList Create(ShoppingListName name, string personId, bool isDone = false, IEnumerable<ShoppingListProduct>? products = null)
            => new (name, personId, isDone, products);

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

            product.Update(new ShoppingListProduct(product.Name, product.Quantity, price, netContent: product.NetContent, isBought: true));
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

            product.Update(new ShoppingListProduct(product.Name, product.Quantity, price, netContent: product.NetContent, isBought: product.IsBought));
        }

        public void CancelPurchaseOfProduct(string productName)
        {
            IsAlreadyDone();

            var product = GetProduct(productName);
            if (product.IsBought == false)
            {
                throw new ShoppingListProductWasNotBoughtException(productName);
            }
            
            product.Update(new ShoppingListProduct(product.Name, product.Quantity, null, netContent: product.NetContent, isBought: false));
        }

        public void MakeListDone()
        {
            IsAlreadyDone();
            IsDone = true;            
        }

        public void UndoListDone()
        {
            IsDone = false;
        }

        public void Update(ShoppingListName shoppingListName)
        {
            Name = shoppingListName;
        }

        public void UpdateProduct(ShoppingListProduct shoppingListProduct, string currentProductName)
        {
            if (shoppingListProduct.Name != currentProductName && _products.Any(p => p.Name == shoppingListProduct.Name))
            {
                throw new ShoppingListProductAlreadyExistsException(shoppingListProduct.Name, Id);
            }

            var product = GetProduct(currentProductName);


            product.Update(shoppingListProduct);
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
