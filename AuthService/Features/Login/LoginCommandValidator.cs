using FluentValidation;

namespace AuthService.Features.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail er påkrævet.")
                .EmailAddress().WithMessage("Ugyldigt e-mailformat.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Adgangskode er påkrævet.");

        }
    }
}
