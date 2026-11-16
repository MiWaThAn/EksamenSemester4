using FluentValidation;
using Ganss.Xss;

namespace ProductCatalogService.Shared.Validation
{

    public static class CustomValidationRules
    {



        public static IRuleBuilderOptions<T, string> NoNullBytes<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(val => string.IsNullOrEmpty(val) || !val.Contains('\0'))
                              .WithMessage("Teksten indeholder ugyldige tegn.");
        }
        public static IRuleBuilderOptions<T, string> NoHTMLTags<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(val => string.IsNullOrEmpty(val) || !val.Contains('<') || !val.Contains('>'))
                              .WithMessage("Teksten indeholder ugyldige tegn.");
        }
        public static IRuleBuilderOptions<T, string> SafeHtmlOnly<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(text =>
            {
                if (string.IsNullOrEmpty(text)) return true;

                var sanitizer = new HtmlSanitizer();
                bool isMaliciousOrUnsafe = false;

                // This handles malicious tags
                sanitizer.RemovingTag += (s, e) =>
                {
                    isMaliciousOrUnsafe = true;
                };

                // This handles malicious attributes
                sanitizer.RemovingAttribute += (s, e) =>
                {
                    isMaliciousOrUnsafe = true;
                };

                // Kør rensningen (den tjekker mod tilladte tags)
                sanitizer.Sanitize(text);

                // Hvis der blev fundet ulovlige ting, returnerer vi false -> validation fejer!
                return !isMaliciousOrUnsafe;

            }).WithMessage("Teksten indeholder skadelig eller ikke-tilladt HTML (f.eks. scripts eller ulovlige tags).");

        }
    }
}
