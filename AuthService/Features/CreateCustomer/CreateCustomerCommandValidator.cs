using AuthService.Shared.Validation;
using FluentValidation;

namespace AuthService.Features.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {

        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.")
                .NoHTMLTags().NoNullBytes();
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.")
                .NoHTMLTags().NoNullBytes();
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.")
                .NoHTMLTags().NoNullBytes();
        }





    }
}
