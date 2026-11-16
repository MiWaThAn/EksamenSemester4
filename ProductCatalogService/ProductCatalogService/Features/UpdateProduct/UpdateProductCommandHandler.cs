using MediatR;
using ProductCatalogService.Repositories;

namespace ProductCatalogService.Features.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                return false;
            }

            
            product.Update(
                request.Name,
                request.Price,
                request.Description,
                request.ImageUrl,
                request.Size,
                request.Color,
                request.Category
            );

           
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
            return true;
        }
    }
}