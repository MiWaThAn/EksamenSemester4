using Dapr.Client;
using ShoppingCartService.Entities;

namespace ShoppingCartService.Repositories.CartRepository
{
    public sealed class DaprCartRepository(DaprClient daprClient) : ICartRepository
    {
        private const string StoreName = "shoppingcartstatestore";

        private static string GetKey(Guid customerId) => $"cart:{customerId}";

       
        public async Task<ShoppingCart?> GetAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
           return await daprClient.GetStateAsync<ShoppingCart>(StoreName, GetKey(customerId), cancellationToken: cancellationToken);
        }
       
        


        public async Task SaveAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            await daprClient.SaveStateAsync(StoreName, GetKey(customerId: cart.CustomerId), cart, cancellationToken: cancellationToken);
        }
            

        public async Task DeleteAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
           await  daprClient.DeleteStateAsync(StoreName, GetKey(customerId), cancellationToken: cancellationToken);
        }
            
    }
}
