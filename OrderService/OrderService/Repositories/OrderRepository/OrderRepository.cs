using Google.Api;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Entities;
namespace OrderService.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly OrderContext _orderContext;
        public OrderRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

       
        
        
        
        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _orderContext.Orders
            .Include(o => o.Lines)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }



        public async Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _orderContext.Orders.AddAsync(order, cancellationToken);
        }

        public async Task SaveChangesAsync()
        {
            await _orderContext.SaveChangesAsync();

        }


    }
}
