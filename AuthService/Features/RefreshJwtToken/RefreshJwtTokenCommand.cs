using AuthService.Features.Shared.Auth;
using MediatR;

namespace AuthService.Features.RefreshJwtToken
{
    public sealed record RefreshJwtTokenCommand : IRequest<AuthDto>
    {
        public string RefreshToken { get; init; } = string.Empty;
        public string? UserAgent { get; init; }
    }
}

