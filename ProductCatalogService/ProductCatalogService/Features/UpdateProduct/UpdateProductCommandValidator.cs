using FluentValidation;
using ProductCatalogService.Shared.Validation;

namespace ProductCatalogService.Features.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Produkt ID er påkrævet.");
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Produktnavn skal udfyldes.")
                .MaximumLength(100).WithMessage("Produktnavn må højst være 100 tegn.")
                .MinimumLength(3).WithMessage("Produktnavn skal være mindst 3 tegn.")
                .NoNullBytes()
                .NoHTMLTags();

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Prisen skal være større end 0.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Beskrivelse skal udfyldes.")
                .NoNullBytes()
                .SafeHtmlOnly();

            

            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Størrelse skal udfyldes.")
                .NoNullBytes()
                .NoHTMLTags();

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Farve skal udfyldes.")
                .NoNullBytes()
                .NoHTMLTags();

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Kategori skal udfyldes.")
                .NoNullBytes()
                .NoHTMLTags();

           
        }
    }
}