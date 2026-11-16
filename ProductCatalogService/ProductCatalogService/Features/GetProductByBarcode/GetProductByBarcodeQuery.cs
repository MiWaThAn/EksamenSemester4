using MediatR;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Features.GetProductByBarcode
{
    public record GetProductByBarcodeQuery(string Barcode) : IRequest<ProductResponse?>;


    
    
}

