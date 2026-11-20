using AuthService.Data;
using AuthService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories.RefreshTokenRepository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthContext _context;

        public RefreshTokenRepository(AuthContext context)
        {
            _context = context;
        }


        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
        }

        public async Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<RefreshToken?>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync(cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenHashWithUserAsync(string tokenHash, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash, cancellationToken);
        }

        public Task UpdateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }


    }
}
