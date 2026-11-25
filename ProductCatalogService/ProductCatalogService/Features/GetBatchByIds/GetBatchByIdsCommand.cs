using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.GetBatchByIds
{
    public  record GetBatchByIdsCommand(IReadOnlyList<Guid> Ids) : IRequest<IReadOnlyList<ProductSummaryDto>>;
}
