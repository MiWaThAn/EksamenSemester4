namespace AuthService.Features.Shared.Customer
{
  public record CustomerAuthDTO(
  Guid Id,
  string Email,
  string Role
);
}
