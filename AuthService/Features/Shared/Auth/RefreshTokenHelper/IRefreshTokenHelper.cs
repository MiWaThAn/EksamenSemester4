using AuthService.Entities;

namespace AuthService.Features.Shared.Auth.RefreshTokenHelper
{
    public interface IRefreshTokenHelper
    {

        Task<string> CreateOrUpdateRefreshToken(Guid userId, string userAgent, CancellationToken cancellationToken);



    }
}
