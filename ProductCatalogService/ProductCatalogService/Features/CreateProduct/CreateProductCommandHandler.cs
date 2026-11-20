using MediatR;
using ProductCatalogService.Entities;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Repositories;

namespace ProductCatalogService.Features.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            Product? existingProduct = await _productRepository.GetByBarcodeAsync(request.Barcode);
            if (existingProduct != null)
            {
                // I need to change this to a result.
                throw new InvalidOperationException("Et produkt med denne stregkode findes allerede.");
            }
            Product product = Product.Create(
                request.Name,
                request.Price,
                request.Description,
                request.ImageUrl,
                request.Size,
                request.Color,
                request.Category,
                request.Barcode
                
            );
            

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return product.ToResponse();
        }
    }
}
