using FluentValidation;


namespace ShoppingCartService.Features.AddToCart
{
    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {

        public AddToCartCommandValidator()
        {

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("KundeId skal udfyldes");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId skal udfyldes");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Mængde skal udfyldes")
                .GreaterThanOrEqualTo(0).WithMessage("Mængden tilføjet skal være positiv eller 0");






        }






    }
}
