using AuthService.Shared.Validation;
using FluentValidation;


namespace AuthService.Features.UpdateCustomerProfile
{
    public class UpdateCustomerProfileCommandValidator : AbstractValidator<UpdateCustomerProfileCommand>
    {
        public UpdateCustomerProfileCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kunde-ID er påkrævet.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Fornavn er påkrævet.")
                .MaximumLength(100).WithMessage("Fornavn må ikke være længere end 100 tegn.").NoHTMLTags().NoNullBytes();

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Efternavn er påkrævet.")
                .MaximumLength(100).WithMessage("Efternavn må ikke være længere end 100 tegn.").NoHTMLTags().NoNullBytes();

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email er påkrævet.")
                .EmailAddress().WithMessage("Ugyldig email-adresse.").NoHTMLTags().NoNullBytes();

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage("Telefonnummer må ikke være længere end 20 tegn.").NoHTMLTags().NoNullBytes();

            RuleFor(x => x.StreetNumber)
                .MaximumLength(20).WithMessage("Vejnummer må ikke være længere end 20 tegn.").NoHTMLTags().NoNullBytes();

            RuleFor(x => x.Street)
                .MaximumLength(100).WithMessage("Vejnavn må ikke være længere end 100 tegn.").NoHTMLTags().NoNullBytes();

            RuleFor(x => x.City)
                .MaximumLength(100).WithMessage("By må ikke være længere end 100 tegn.").NoHTMLTags().NoNullBytes();

            RuleFor(x => x.ZipCode)
                .MaximumLength(10).WithMessage("Postnummer må ikke være længere end 10 tegn.").NoHTMLTags().NoNullBytes();

            RuleFor(x => x.Country)
                .MaximumLength(100).WithMessage("Land må ikke være længere end 100 tegn.").NoHTMLTags().NoNullBytes();
           
            RuleFor(x => x)
                .Must(x =>
                {
                    var fields = new[] { x.StreetNumber, x.Street, x.City, x.ZipCode, x.Country };
                    var filledCount = fields.Count(f => !string.IsNullOrWhiteSpace(f));
                    return filledCount == 0 || filledCount == fields.Length;
                })
                .WithMessage("Hvis du vil tilføje eller opdatere en adresse, skal alle adressefelter udfyldes.")
                .OverridePropertyName("Address");
        }
    }
}