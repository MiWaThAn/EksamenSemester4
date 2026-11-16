using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse?>;

}
