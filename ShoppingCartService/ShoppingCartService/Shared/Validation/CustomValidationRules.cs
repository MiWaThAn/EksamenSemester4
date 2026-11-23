using FluentValidation;

namespace ShoppingCartService.Shared.Validation
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
       



    }
}
