namespace AuthService.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public string? UserAgent { get; private set; }
        public string TokenHash { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public string? ReplacedByTokenHash { get; private set; }

        public User User { get; private set; } = null!;

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsRevoked => RevokedAt is not null;
        public bool IsActive => !IsRevoked && !IsExpired;

        private RefreshToken() { } // For EF Core only

        public RefreshToken(Guid userId, string tokenHash, DateTime expiresAt, string? userAgent)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            if (string.IsNullOrWhiteSpace(tokenHash))
                throw new ArgumentException("TokenHash cannot be empty.", nameof(tokenHash));
            if (expiresAt <= DateTime.UtcNow)
                throw new ArgumentException("ExpiresAt must be in the future.", nameof(expiresAt));

            UserId = userId;
            TokenHash = tokenHash;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt;
            ShortenUserAgentIfTooLong(userAgent);
        }

        public void Revoke(string? replacedByTokenHash = null)
        {
            if (RevokedAt is not null)
                return;

            RevokedAt = DateTime.UtcNow;
            ReplacedByTokenHash = replacedByTokenHash;
            
        }

        private void ShortenUserAgentIfTooLong(string? userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
            {
                UserAgent = null;
                return;
            }

            UserAgent = userAgent.Length > 255
                ? userAgent[..255]
                : userAgent;
        }



    }
}