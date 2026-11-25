namespace OrderService.Entities
{
    public class OrderLine
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal LineTotal => UnitPrice * Quantity;

        private OrderLine() { } //For EF Core only

        private OrderLine(Guid productId, string productName, decimal unitPrice, int quantity)
        {
          
            SetProductId(productId);
            SetProductName(productName);
            SetUnitPrice(unitPrice);
            SetQuantity(quantity);


        }

        public static OrderLine Create(Guid productId, string productName, decimal unitPrice, int quantity)
        {

            return new OrderLine(productId, productName, unitPrice, quantity);

        }

        private void SetProductId(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentException("ProductId is required.", nameof(productId));
            }

            ProductId = productId;
        }

        private void SetProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new ArgumentException("ProductName is required", nameof(productName));
            }

            ProductName = productName;
        }

        private void SetUnitPrice(decimal unitPrice)
        {
            if (unitPrice < 0)
            {
                throw new ArgumentOutOfRangeException("unitPrice can not be below zero.", nameof(unitPrice));

            }
            UnitPrice = unitPrice;
        }

        private void SetQuantity(int quantity)
        {
            if (quantity < 1)
            {
                throw new ArgumentOutOfRangeException("Quantity can not be less than 1", nameof(quantity));
            }
            Quantity = quantity;
        }

    }
}
