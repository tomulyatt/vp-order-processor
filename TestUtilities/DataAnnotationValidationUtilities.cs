using System.ComponentModel.DataAnnotations;

namespace TestUtilities
{
    public static class DataAnnotationValidationUtilities
    {
        public static bool IsValid<T>(this T model) where T : class
        {
            ValidationContext validationContext = new ValidationContext(model);
            List<ValidationResult> valdiationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, validationContext, valdiationResults, true);
        }

        public static IList<ValidationResult> ValidateModel<T>(this T model) where T : class
        {
            ValidationContext validationContext = new ValidationContext(model);
            IList<ValidationResult> validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        public static bool IsValid<T>(this T model, IServiceProvider? serviceProvider = null) where T : class
        {
            ValidationContext validationContext = new ValidationContext(model, serviceProvider: serviceProvider, default);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, validationContext, validationResults, true);
        }

        public static IList<ValidationResult> ValidateModel<T>(this T model, IServiceProvider? serviceProvider = null) where T : class
        {
            ValidationContext validationContext = new ValidationContext(model, serviceProvider: serviceProvider, default);
            IList<ValidationResult> validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
