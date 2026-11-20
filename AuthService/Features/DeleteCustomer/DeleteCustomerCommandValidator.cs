using FluentValidation;
using MediatR;

namespace AuthService.Features.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kunde-ID er påkrævet.");
        }
    }
}