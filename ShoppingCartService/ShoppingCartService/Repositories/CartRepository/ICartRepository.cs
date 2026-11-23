using ShoppingCartService.Entities;

namespace ShoppingCartService.Repositories.CartRepository
{
    public interface ICartRepository
    {

        Task<ShoppingCart?> GetAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task SaveAsync(ShoppingCart cart, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid customerId, CancellationToken cancellationToken = default);


    }
}
