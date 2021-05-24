using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RoomValidator:AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(50);
        }   
    }
}