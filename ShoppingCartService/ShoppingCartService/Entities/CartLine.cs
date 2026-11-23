using System.Text.Json.Serialization;

namespace ShoppingCartService.Entities
{
    public class CartLine
    {
        [JsonInclude]
        public Guid ProductId { get; private set; }
        [JsonInclude]
        public int Quantity { get; private set; }

        [JsonConstructor]
        private CartLine() { } 

        private CartLine(Guid productId, int quantity)
        {
            SetProductId(productId);
            SetQuantity(quantity);
           
        }

        public static CartLine Create(Guid productId, int quantity)
        {
            return new CartLine(productId, quantity);
        }

        internal void AddQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Antal skal være større end 0.", nameof(quantity));

            Quantity += quantity;
        }
        
        private void SetProductId(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId må ikke være tom.", nameof(productId));
            ProductId = productId;
        }

        internal void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Antal skal være større end 0.", nameof(quantity));

            Quantity = quantity;
        }


    }
}

