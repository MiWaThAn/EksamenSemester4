using AuthService.Entities;

namespace AuthService.Features.Shared.Auth.RefreshTokenHelper
{
    public interface IRefreshTokenHelper
    {

        Task<string> CreateOrUpdateRefreshToken(Guid userId, string userAgent, CancellationToken cancellationToken);
        Task<AuthDto> RotateRefreshTokenAsync(string refreshToken, string? userAgent, CancellationToken cancellationToken);
        Task RevokeAllRefreshTokens(Guid userId, CancellationToken cancellationToken);
        Task RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken);


    }
}
