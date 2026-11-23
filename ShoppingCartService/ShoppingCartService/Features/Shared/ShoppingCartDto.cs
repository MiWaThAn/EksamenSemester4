namespace ShoppingCartService.Features.Shared
{
    public record ShoppingCartDto(Guid CustomerId, IReadOnlyList<CartLineDto> Lines);
}
