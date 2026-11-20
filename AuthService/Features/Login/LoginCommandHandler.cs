using AuthService.Entities;
using AuthService.Features.Shared.Auth;
using AuthService.Features.Shared.Auth.RefreshTokenHelper;
using AuthService.Repositories;
using AuthService.Repositories.RefreshTokenRepository;
using AuthService.Shared.Security.RefreshTokenHasher;
using MediatR;

namespace AuthService.Features.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenHelper _refreshTokenHelper;

        public LoginCommandHandler(IUserRepository userRepository, IRefreshTokenHelper refreshTokenHelper)
        {
            _userRepository = userRepository;
            _refreshTokenHelper = refreshTokenHelper;
        }

        public async Task<AuthDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var user = await _userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHashed))
            {
                throw new UnauthorizedAccessException("Ugyldig e-mail eller adgangskode.");
            }


            
            var refreshToken = await _refreshTokenHelper.CreateOrUpdateRefreshToken(user.Id, request.UserAgent, cancellationToken);



            if (user is Employee employee)
            {
                return new AuthDto(
                    employee.Id,
                    employee.Email,
                    employee.Role.ToString(),
                    employee.Permissions,
                    refreshToken
                );
            }
            else
            {
                return new AuthDto(
                    user.Id,
                    user.Email,
                    user.Role.ToString(),
                    null,
                    refreshToken
                );
            }
        }
    }
}
