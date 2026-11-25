using MediatR;
using ProductCatalogService.Repositories;
namespace ProductCatalogService.Features.GetBatchByIds
{
    public class GetBatchByIdsCommandHandler : IRequestHandler<GetBatchByIdsCommand, IReadOnlyList<ProductSummaryDto>>
    {

        private readonly IProductRepository _productRepository;
        public GetBatchByIdsCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<IReadOnlyList<ProductSummaryDto>> Handle(GetBatchByIdsCommand request, CancellationToken cancellationToken)
        {
            var distinctIds = request.Ids
                .Where(id => id != Guid.Empty)
                .Distinct()
                .ToList();

            if (distinctIds.Count == 0)
            {
                return [];
            }

            var products = await _productRepository.GetByIdsAsync(distinctIds, cancellationToken);

            var summaries = products
                .Select(p => new ProductSummaryDto(p.Id, p.Name, p.ImageUrl, p.Price))
                .ToList();

            return summaries.AsReadOnly();
        }
    }
}
