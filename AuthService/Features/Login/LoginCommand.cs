using AuthService.Features.Shared.Auth;
using MediatR;

namespace AuthService.Features.Login
{
    public record LoginCommand : IRequest<AuthDto>
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string? UserAgent { get; init; }

        public LoginCommand(string email, string password, string userAgent)
        {
            Email = email.Trim() ?? string.Empty;
            Password = password ?? string.Empty;
            NormalizeUserAgent(userAgent);
        }

        private static string? NormalizeUserAgent(string? userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
                return null;

            var trimmed = userAgent.Trim();
            return trimmed.Length > 255 ? trimmed[..255] : trimmed;
        }


    }
         
}
