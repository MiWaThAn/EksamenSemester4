using AuthService.Features.Shared.Auth;
using AuthService.Features.Shared.Auth.RefreshTokenHelper;
using MediatR;

namespace AuthService.Features.RefreshJwtToken
{
    public  class RefreshJwtTokenCommandHandler : IRequestHandler<RefreshJwtTokenCommand, AuthDto>
    {
        private readonly IRefreshTokenHelper _refreshTokenHelper;

        public RefreshJwtTokenCommandHandler(IRefreshTokenHelper refreshTokenHelper)
        {
            _refreshTokenHelper = refreshTokenHelper;
        }

        public async Task<AuthDto> Handle(RefreshJwtTokenCommand request, CancellationToken cancellationToken)
        {
            return await _refreshTokenHelper.RotateRefreshTokenAsync(
                request.RefreshToken,
                request.UserAgent,
                cancellationToken);
        }
    }
}
