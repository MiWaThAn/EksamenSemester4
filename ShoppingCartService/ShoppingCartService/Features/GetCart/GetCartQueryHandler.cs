using MediatR;
using ShoppingCartService.Clients.ProductCatalogClient;
using ShoppingCartService.Entities;
using ShoppingCartService.Features.Shared;
using ShoppingCartService.Repositories.CartRepository;

namespace ShoppingCartService.Features.GetCart
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, ShoppingCartDto>
    {
        private readonly IProductCatalogClient _productCatalogClient;
        private readonly ICartRepository _cartRepository;
        public GetCartQueryHandler(ICartRepository cartRepository, IProductCatalogClient productCatalogClient)
        {
            _cartRepository = cartRepository;
            _productCatalogClient = productCatalogClient;
        }


        public async Task<ShoppingCartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetAsync(request.CustomerId, cancellationToken)
                       ?? ShoppingCart.Create(request.CustomerId);

            if (cart.Lines.Count == 0)
            {
                return new ShoppingCartDto(request.CustomerId, []);
            }

            var lineIds = cart.Lines
                .Select(line => line.ProductId)
                .Distinct()
                .ToList();

            var products = await _productCatalogClient.GetProductsByIdsAsync(lineIds, cancellationToken);
            var productLookup = products.ToDictionary(p => p.Id);

            var cartLineDtos = new List<CartLineDto>(cart.Lines.Count);

            foreach (var line in cart.Lines)
            {
                if (!productLookup.TryGetValue(line.ProductId, out var product))
                {
                    continue;
                }

                var lineTotal = product.Price * line.Quantity;

                cartLineDtos.Add(new CartLineDto(
                    ProductId: line.ProductId,
                    ProductName: product.Name,
                    ImageUrl: product.ImageUrl,
                    Price: product.Price,
                    Quantity: line.Quantity,
                    LineTotal: lineTotal));
            }

            return new ShoppingCartDto(request.CustomerId, cartLineDtos.AsReadOnly());
        }
    }
}
