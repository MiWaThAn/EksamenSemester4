using MediatR;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Repositories;

namespace ProductCatalogService.Features.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                return null;
            }

           
            return product.ToResponse();
        }
    }
}
