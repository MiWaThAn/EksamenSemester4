using FluentValidation;

namespace ShoppingCartService.Features.GetCart
{
    public class GetCartQueryValidator : AbstractValidator<GetCartQuery>
    {
        public GetCartQueryValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("KundeId skal udfyldes");
        }
    }
}
