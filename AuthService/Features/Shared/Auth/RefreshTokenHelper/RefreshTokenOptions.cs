namespace AuthService.Features.Shared.Auth.RefreshTokenHelper
{
    public sealed class RefreshTokenOptions
    {
        public int LifetimeDays { get; init; } = 14;
    }
}
