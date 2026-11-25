namespace ProductCatalogService.Features.GetBatchByIds
{
    public record ProductSummaryDto (Guid Id, string Name, string? ImageUrl, decimal Price);
    
}
