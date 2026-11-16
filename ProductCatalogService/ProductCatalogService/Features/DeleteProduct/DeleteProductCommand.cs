using MediatR;

namespace ProductCatalogService.Features.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;
}