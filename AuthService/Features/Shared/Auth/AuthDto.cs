namespace AuthService.Features.Shared.Auth
{
    public record AuthDto(
        Guid Id,
        string Email,
        string Role,
        IReadOnlyCollection<string>? Permissions,
        string RefreshToken
    );
}
