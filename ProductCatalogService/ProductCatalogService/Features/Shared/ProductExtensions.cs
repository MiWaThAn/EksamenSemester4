using ProductCatalogService.Entities;

namespace ProductCatalogService.Features.Shared
{
    public static class ProductExtensions
    {
        public static ProductResponse ToResponse(this Product product)
        {
            return new ProductResponse(
                product.Id,
                product.Name,
                product.Price,
                product.Description,
                product.ImageUrl,
                product.Size,
                product.Color,
                product.Category,
                product.Barcode,
                product.InStock
            );
        }
    }
}
