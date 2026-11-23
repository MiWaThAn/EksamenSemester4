using System.Text.Json.Serialization;

namespace ShoppingCartService.Entities
{
    public class ShoppingCart
    {
        [JsonInclude]
        private  List<CartLine> _lines { get; set; } = [];
        [JsonInclude]
        public Guid CustomerId { get; private set; }
        [JsonIgnore]
        public IReadOnlyCollection<CartLine> Lines => _lines.AsReadOnly();

        [JsonConstructor]
        private ShoppingCart() { }

        private ShoppingCart(Guid customerId)
        {
            SetCustomerId(customerId);
        }

        public static ShoppingCart Create(Guid customerId)
        {
            

            return new ShoppingCart(customerId);
        }

        public void SetItemQuantity(Guid productId, int quantity)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId må ikke være tom.", nameof(productId));

            if (quantity < 0)
                throw new ArgumentException("Antal må ikke være negativt.", nameof(quantity));

            var existing = _lines.FirstOrDefault(x => x.ProductId == productId);

            if (quantity == 0)
            {
                if (existing is not null)
                    _lines.Remove(existing);
                return;
            }

            if (existing is null)
            {
                _lines.Add(CartLine.Create(productId, quantity));
            }
            else
            {
                existing.SetQuantity(quantity);
            }
        }

        private void SetCustomerId(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("CustomerId må ikke være tom.", nameof(customerId));
            CustomerId = customerId;
        }

        public void ClearCart() => _lines.Clear();










    }
}
