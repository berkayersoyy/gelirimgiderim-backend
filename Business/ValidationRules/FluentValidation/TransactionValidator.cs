using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TransactionValidator:AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {
            RuleFor(t => t.Amount).NotEqual(0);
            RuleFor(t => t.Description).MaximumLength(50);
            RuleFor(c => c.Title).NotEmpty();
        }
    }
}