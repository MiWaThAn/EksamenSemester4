
namespace ShoppingCartService.Clients.ProductCatalogClient
{
    
    

    public record BatchProductsRequest(IReadOnlyList<Guid> Ids);



    public sealed class HttpProductCatalogClient(HttpClient httpClient) : IProductCatalogClient
    {
        public async Task<IReadOnlyList<ProductSummaryDto>> GetProductsByIdsAsync(
            IReadOnlyList<Guid> ids,
            CancellationToken cancellationToken = default)
        {
            var distinctIds = ids
                .Where(id => id != Guid.Empty)
                .Distinct()
                .ToList();

            if (distinctIds.Count == 0)
            {
                return [];
            }

            using var response = await httpClient.PostAsJsonAsync(
                "api/products/BatchQuery",
                new BatchProductsRequest(distinctIds),
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<List<ProductSummaryDto>>(
                cancellationToken: cancellationToken);

            return products?.AsReadOnly() ?? [];
        }
    }
}