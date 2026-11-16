using MediatR;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Repositories;

namespace ProductCatalogService.Features.Products.GetAllProducts
{
    public class GetPagedProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, PagedResult<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetPagedProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductResponse>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
           
            var (products, totalCount) = await _productRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);

            
            var productResponses = products.Select(p => p.ToResponse()).ToList();

            
            return new PagedResult<ProductResponse>
            {
                Items = productResponses,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}