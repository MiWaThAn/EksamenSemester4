using FluentValidation;
using ProductCatalogService.Features.CreateProduct;

namespace ProductCatalogService.Features.GetBatchByIds
{
    public class GetBatchByIdsCommandValidator : AbstractValidator<GetBatchByIdsCommand>
    {
        public GetBatchByIdsCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotNull().WithMessage("Id-listen må ikke være null.")
                .NotEmpty().WithMessage("Der skal angives mindst ét produkt-ID.");

            RuleForEach(x => x.Ids)
                .NotEqual(Guid.Empty).WithMessage("Produkt-ID må ikke være tomt.");
        }
    }
}