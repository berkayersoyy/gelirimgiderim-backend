using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        /// <summary>
        /// Validates the entity with FluentValidation.
        /// </summary>
        /// <param name="validator">Validator Type</param>
        /// <param name="entity">Entity</param>
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}