using Grpc.Core;
using OrderService.Entities;
using OrderService.Entities.Value_Objects;
using OrderService.Repositories.OrderRepository;

namespace OrderService.Data
{
    public class OrderDbSeeding
    {

        private readonly IOrderRepository _orderRepository;

        public OrderDbSeeding(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task SeedAsync(OrderContext dbContext, CancellationToken cancellationToken = default)
        {

            var seedOrderId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var existingOrder = await _orderRepository.GetByIdAsync(seedOrderId, cancellationToken);

            if (existingOrder is not null)
            {
                return;
            }

            var customerId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var address = Address.Create(
            streetNumber: "10",
            street: "Hovedgaden",
            city: "Vejle",
            zipCode: "7100",
            country: "Danmark");

           
            
            var lines = new List<OrderLine>
            {
            OrderLine.Create(
                productId: Guid.Parse("33333333-3333-3333-3333-333333333333"),
                productName: "Test Produkt",
                unitPrice: 249.00m,
                quantity: 2
            )};

            var order = Order.Create(
                customerId: customerId,
                customerName: "Test Kunde",
                billingAddress: address,
                deliveryAddress: address,
                lines: lines);

            await _orderRepository.AddAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync();



        }




}
}
