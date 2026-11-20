using AuthService.Features.Shared.Auth;
using MediatR;

namespace AuthService.Features.Login
{
    public record LoginCommand : IRequest<AuthDto>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string UserAgent { get; private set; }

        public LoginCommand(string email, string password, string userAgent)
        {
            Email = email.Trim() ?? string.Empty;
            Password = password.Trim() ?? string.Empty;
            ShortenUserAgent(userAgent);
        }

        private void ShortenUserAgent(string userAgent)
        {
            if (userAgent.Length > 255)
            {
                UserAgent = userAgent.Substring(0, 255);
            }
        }


    }
         
}
