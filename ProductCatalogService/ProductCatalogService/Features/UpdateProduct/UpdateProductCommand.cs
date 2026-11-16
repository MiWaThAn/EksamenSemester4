using MediatR;
namespace ProductCatalogService.Features.Products.UpdateProduct

{
    
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        double Price,
        string Description,
        string? ImageUrl,
        string Size,
        string Color,
        string Category
    ) : IRequest<bool>; 
}