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
           await _refreshTokenRepository.AddAsync(new RefreshToken(userId, hashedRefreshToken, lifetime, userAgent), cancellationToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);
            return refreshToken;

        }

        public async Task<AuthDto> RotateRefreshTokenAsync(string rawRefreshToken, string? userAgent, CancellationToken cancellationToken)
        {
            var incomingHash = _refreshTokenHasher.HashRawRefreshToken(rawRefreshToken);
            var tokenEntity = await _refreshTokenRepository.GetByTokenHashWithUserAsync(incomingHash, cancellationToken);

            if (tokenEntity is null)
                throw new UnauthorizedAccessException("Ugyldigt refresh token.");

            if (tokenEntity.IsRevoked)
            {
                await RevokeAllRefreshTokens(tokenEntity.UserId, cancellationToken);
                throw new UnauthorizedAccessException("Refresh token er allerede brugt. Alle sessioner er tilbagekaldt.");
            }

            if (tokenEntity.IsExpired)
                throw new UnauthorizedAccessException("Refresh token er udløbet.");

            var newRawToken = _refreshTokenHasher.GenerateRawRefreshToken();
            var newHashedToken = _refreshTokenHasher.HashRawRefreshToken(newRawToken);
            var expiresAt = DateTime.UtcNow.AddDays(_refreshTokenOptions.LifetimeDays);

            var nextTokenEntity = new RefreshToken(tokenEntity.UserId, newHashedToken, expiresAt, userAgent);
            await _refreshTokenRepository.AddAsync(nextTokenEntity, cancellationToken);

            tokenEntity.Revoke(newHashedToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            var user = tokenEntity.User;
            IReadOnlyCollection<string>? permissions = user is Employee employee
                ? employee.Permissions
                : null;

            return new AuthDto(
                user.Id,
                user.Email,
                user.Role.ToString(),
                permissions,
                newRawToken
            );
        }




        public async Task RevokeRefreshToken(string rawRefreshToken, CancellationToken cancellationToken)
        {
            var hashedRefreshToken = _refreshTokenHasher.HashRawRefreshToken(rawRefreshToken);
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
