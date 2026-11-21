using AuthService.Features.Shared.Auth.RefreshTokenHelper;
using MediatR;

namespace AuthService.Features.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IRefreshTokenHelper _refreshTokenHelper;

        public LogoutCommandHandler(IRefreshTokenHelper refreshTokenHelper)
        {
            _refreshTokenHelper = refreshTokenHelper;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.refreshToken))
            {
                return false;
            }

            try
            {
                await _refreshTokenHelper.RevokeRefreshToken(request.refreshToken, cancellationToken);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}