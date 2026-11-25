namespace ShoppingCartService.Clients.ProductCatalogClient
{
    public interface IProductCatalogClient
    {
        Task<IReadOnlyList<ProductSummaryDto>> GetProductsByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken = default);
    }
}
