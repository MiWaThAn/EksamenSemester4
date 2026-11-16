using MediatR;
using ProductCatalogService.Features.GetProductById;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Repositories;

namespace ProductCatalogService.Features.GetProductByBarcode
{
    public class GetProductByBarcodeQueryHandler : IRequestHandler<GetProductByBarcodeQuery, ProductResponse?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByBarcodeQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse?> Handle(GetProductByBarcodeQuery request, CancellationToken cancellationToken)
        {

            var product = await _productRepository.GetByBarcodeAsync(request.Barcode);

            if (product == null)
            {
                return null;
            }

            
            return product.ToResponse();
        }
    }
}

                
           
