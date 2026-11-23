using FluentValidation;

namespace ShoppingCartService.Features.ClearCart
{
    public class ClearCartCommandValidator : AbstractValidator<ClearCartCommand>
    {
        public ClearCartCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("KundeId skal udfyldes");
        }
    }
}
