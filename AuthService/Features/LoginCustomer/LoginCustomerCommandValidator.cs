using AuthService.Features.Shared.Customer;
using FluentValidation;
using MediatR;

namespace AuthService.Features.LoginCustomer
{
    public class LoginCustomerCommandValidator : AbstractValidator<LoginCustomerCommand>
    {
        public LoginCustomerCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail er påkrævet.")
                .EmailAddress().WithMessage("Ugyldigt e-mailformat.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Adgangskode er påkrævet.");
        }
    }
}