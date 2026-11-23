using MediatR;

namespace ShoppingCartService.Features.ClearCart
{
    public record ClearCartCommand(Guid CustomerId) : IRequest<bool>;
}
