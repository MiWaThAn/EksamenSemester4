using AuthService.Entities;

namespace AuthService.Repositories.RefreshTokenRepository
{
    public interface IRefreshTokenRepository
    {



        Task AddAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<RefreshToken?>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<RefreshToken?> GetByTokenHashWithUserAsync(string tokenHash, CancellationToken cancellationToken);
        Task UpdateAsync(RefreshToken refreshToken);
        Task DeleteAsync(RefreshToken refreshToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
