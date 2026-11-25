namespace ShoppingCartService.Features.Shared
{
    public record CartLineDto(Guid ProductId, string ProductName,
    string? ImageUrl,
    decimal Price,
    int Quantity,
    decimal LineTotal);
}
