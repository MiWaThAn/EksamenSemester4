namespace ProductCatalogService.Features.Shared
{
    public record ProductResponse(
        Guid Id,
        string Name,
        decimal Price,
        string Description,
        string? ImageUrl,
        string Size,
        string Color,
        string Category,
        string Barcode,
        bool? InStock
    );
}
