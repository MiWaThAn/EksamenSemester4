using System.Security.Cryptography;
using System.Text;

namespace AuthService.Shared.Security.RefreshTokenHasher
{
    public class RefreshTokenHasher : IRefreshTokenHasher
    {
        public string GenerateRawRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }

        public string HashRawRefreshToken(string rawRefreshToken)
        {
            var bytes = Encoding.UTF8.GetBytes(rawRefreshToken);
            var hashBytes = SHA256.HashData(bytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
