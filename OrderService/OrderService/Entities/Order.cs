using OrderService.Entities.Enums;
using OrderService.Entities.Value_Objects;
namespace OrderService.Entities
{
    public class Order
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public Address BillingAddress { get; private set; }
        public Address DeliveryAddress { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }
        public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();
        private readonly List<OrderLine> _lines = new();

        private Order() { } //For EF core only
        private Order(Guid customerId, string customerName, Address billingAddress, Address deliveryAddress, IEnumerable<OrderLine> lines)
        {
           
            SetCustomerId(customerId);
            SetCustomerName(customerName);
            BillingAddress = billingAddress;
           DeliveryAddress = deliveryAddress;
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Pending;

            var lineList = lines?.ToList() ?? throw new ArgumentNullException(nameof(lines));
            if (lineList.Count == 0)
            {
                throw new ArgumentException("Order can not be empty", nameof(lines));
            }

            SetOrderLines(lineList);
            SetTotalPrice(lineList);

            
        }

        public static Order Create(Guid customerId, string customerName, Address billingAddress, Address deliveryAddress, IEnumerable<OrderLine> lines)
        {
            return new Order(customerId, customerName, billingAddress, deliveryAddress, lines);
        }

        
        private void SetCustomerId(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("CustomerId is required", nameof(customerId));
            }

            CustomerId = customerId;
        }
        private void SetCustomerName(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                throw new ArgumentException("CustomerName is required", nameof(customerName));
            }
            CustomerName = customerName;
        }

        private void SetOrderLines(IEnumerable<OrderLine> lineList)
        {
            
            _lines.AddRange(lineList);

        }


        private void SetTotalPrice(IEnumerable<OrderLine> lineList)
        {
            decimal total = 0;
            foreach (var line in lineList)
            {
                 total += line.LineTotal;
            }

            TotalPrice = total;

        }

        public void MarkAsReserved() => Status = OrderStatus.Reserved; //Ill be using this once confirmation of item reservation has been received

        public void MarkAsConfirmed() => Status = OrderStatus.Confirmed; // Ill be using this once payment has been confirmed.

        public void MarkAsCancelled() => Status = OrderStatus.Cancelled; //Ill be using this if customer decides to cancel the order before payment has been confirmed.
        public void MarkAsFailed() => Status = OrderStatus.Failed; // Ill be using this if something fails along the way, or if payment did not go through.


    }
}
