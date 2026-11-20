namespace AuthService.Shared.Security.RefreshTokenHasher
{
    public interface IRefreshTokenHasher
    {
        string GenerateRawRefreshToken();
        string HashRawRefreshToken(string rawRefreshToken);

    }
}
