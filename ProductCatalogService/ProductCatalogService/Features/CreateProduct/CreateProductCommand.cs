using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.CreateProduct
{
    public record CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public string Size { get; init; }
        public string Color { get; init; }
        public string Category { get; init; }
        public string Barcode { get; init; }

        
        public CreateProductCommand(string name, decimal price, string description, string imageUrl, string size, string color, string category, string barcode)
        {
            Name = name?.Trim() ?? string.Empty;
            Price = price;
            Description = description?.Trim() ?? string.Empty;
            ImageUrl = imageUrl?.Trim() ?? string.Empty;
            Size = size?.Trim() ?? string.Empty;
            Color = color?.Trim() ?? string.Empty;
            Category = category?.Trim() ?? string.Empty;
            Barcode = barcode?.Trim() ?? string.Empty;
        }
    }
}