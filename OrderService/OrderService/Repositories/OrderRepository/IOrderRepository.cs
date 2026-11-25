using OrderService.Data;
using OrderService.Entities;

namespace OrderService.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Order order, CancellationToken cancellationToken);

        Task SaveChangesAsync();

    }
}
