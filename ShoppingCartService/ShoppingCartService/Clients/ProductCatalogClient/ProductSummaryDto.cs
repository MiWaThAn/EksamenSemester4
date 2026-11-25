namespace ShoppingCartService.Clients.ProductCatalogClient
{
    public  record ProductSummaryDto(Guid Id, string Name, string? ImageUrl, decimal Price);
}
