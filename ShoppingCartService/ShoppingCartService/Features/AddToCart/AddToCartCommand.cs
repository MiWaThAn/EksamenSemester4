using MediatR;

namespace ShoppingCartService.Features.AddToCart
{
    public record AddToCartCommand : IRequest<bool>
    {
        public Guid CustomerId { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }


    }
}
