using MediatR;
using ShoppingCartService.Entities;
using ShoppingCartService.Features.Shared;
using ShoppingCartService.Repositories.CartRepository;

namespace ShoppingCartService.Features.GetCart
{
    public  class GetCartQueryHandler: IRequestHandler<GetCartQuery, ShoppingCartDto>
    {
        private readonly ICartRepository _cartRepository;
        public GetCartQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ShoppingCartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetAsync(request.CustomerId, cancellationToken)
                       ?? ShoppingCart.Create(request.CustomerId);

            var lines = cart.Lines
                .Select(line => new CartLineDto(line.ProductId, line.Quantity))
                .ToList();

            return new ShoppingCartDto(cart.CustomerId, lines);
        }
    }
}
