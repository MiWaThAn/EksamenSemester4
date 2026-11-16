using FluentValidation;
using Ganss.Xss;
using ProductCatalogService.Shared.Validation;
namespace ProductCatalogService.Features.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        
        


        public CreateProductCommandValidator()
        {
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

            //Don't know if I will add any rules for imageurl

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

            RuleFor(x => x.Barcode)
               .NotEmpty().WithMessage("Stregkode er påkrævet.")
               .Length(8, 13).WithMessage("Stregkode skal være mellem 8 og 13 tegn.")
               .NoNullBytes()
               .NoHTMLTags();

            //I think the Instock property should instead fire event for checking with WareHouse?

        }

        
    }
}
