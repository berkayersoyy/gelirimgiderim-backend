using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TransactionValidator:AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {
            RuleFor(t => t.Amount).NotEqual(0);
            RuleFor(t => t.CategoryId).NotEmpty();
            RuleFor(t => t.Description).NotEmpty();
            RuleFor(t => t.Description).MaximumLength(50);
        }
    }
}