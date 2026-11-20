using AuthService.Entities;
using AuthService.Repositories.RefreshTokenRepository;
using AuthService.Shared.Security.RefreshTokenHasher;

namespace AuthService.Features.Shared.Auth.RefreshTokenHelper
{
    public class RefreshTokenHelper : IRefreshTokenHelper
    {
        private readonly IRefreshTokenHasher _refreshTokenHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly RefreshTokenOptions _refreshTokenOptions;




        public RefreshTokenHelper(IRefreshTokenHasher refreshTokenHasher, IRefreshTokenRepository refreshTokenRepository, RefreshTokenOptions refreshTokenOptions)
        {
            _refreshTokenHasher = refreshTokenHasher;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenOptions = refreshTokenOptions;
        }

        public async Task<string> CreateOrUpdateRefreshToken(Guid userId, string userAgent, CancellationToken cancellationToken)
        {
            

            var refreshToken = _refreshTokenHasher.GenerateRawRefreshToken();
            var hashedRefreshToken = _refreshTokenHasher.HashRawRefreshToken(refreshToken);

            


            var lifetime = DateTime.UtcNow.AddDays(_refreshTokenOptions.LifetimeDays);
           await _refreshTokenRepository.AddAsync(new RefreshToken(userId, hashedRefreshToken, lifetime, userAgent));

            return refreshToken;

        }
        public async Task RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var hashedRefreshToken = _refreshTokenHasher.HashRawRefreshToken(refreshToken);
            var tokenEntity = await _refreshTokenRepository.GetByTokenHashWithUserAsync(hashedRefreshToken, cancellationToken);

            if (tokenEntity is null)
            {
                throw new InvalidOperationException("Refresh token not found.");
            }

            tokenEntity.Revoke();
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task RevokeAllRefreshTokens(Guid userId, CancellationToken cancellationToken)
        {
            var existingTokens = await _refreshTokenRepository.GetByUserIdAsync(userId, cancellationToken);

            foreach (var token in existingTokens)
            {
                if (token is not null && token.RevokedAt is null)
                {
                    token.Revoke();
                }
            }

            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
