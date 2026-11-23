namespace ShoppingCartService.Features.Shared
{
    public record CartLineDto(Guid ProductId, int Quantity);
}
