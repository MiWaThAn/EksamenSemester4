using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.Products.GetAllProducts
{
    public record GetPagedProductsQuery(int PageNumber = 1, int PageSize = 20) : IRequest<PagedResult<ProductResponse>>;
}