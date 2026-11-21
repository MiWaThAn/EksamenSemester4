using FluentValidation;

namespace AuthService.Features.RefreshJwtToken
{
    public class RefreshJwtTokenCommandValidator : AbstractValidator<RefreshJwtTokenCommand>
    {
        public RefreshJwtTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token er påkrævet.");
        
        
        }



    }
}
