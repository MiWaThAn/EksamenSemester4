using MediatR;
using ShoppingCartService.Features.Shared;

namespace ShoppingCartService.Features.GetCart
{
    public record GetCartQuery(Guid CustomerId) : IRequest<ShoppingCartDto>;
}
