using FluentValidation;

namespace AuthService.Features.Logout
{
    public class LogoutCommandValidator :AbstractValidator<LogoutCommand>
    {

        public LogoutCommandValidator()
        {
            RuleFor(x => x.refreshToken)
                .NotEmpty().WithMessage("Refresh token er påkrævet.");


        }
    }
}
