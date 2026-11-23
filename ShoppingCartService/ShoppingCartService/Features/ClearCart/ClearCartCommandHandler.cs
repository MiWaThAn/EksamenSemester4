using MediatR;
using ShoppingCartService.Repositories.CartRepository;

namespace ShoppingCartService.Features.ClearCart
{
    public sealed class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, bool>
    {

        private readonly ICartRepository _cartRepository;
        public ClearCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetAsync(request.CustomerId, cancellationToken);
            if (cart is null)
            {
                return true;
            }

           
            await _cartRepository.DeleteAsync(request.CustomerId, cancellationToken);
            return true;
        }
    }
}
