using MediatR;
using ShoppingCartService.Entities;
using ShoppingCartService.Repositories.CartRepository;



namespace ShoppingCartService.Features.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, bool>
    {
        
        private readonly ICartRepository _cartRepository;
        
        public AddToCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetAsync(request.CustomerId, cancellationToken)
            ?? ShoppingCart.Create(request.CustomerId);

            cart.SetItemQuantity(request.ProductId, request.Quantity);

            await _cartRepository.SaveAsync(cart, cancellationToken);
            return true;

            
        }




    }
}
