using Application.DataTransferObjects;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientDTO>
    {
        public UpdateClientValidator()
        {
            RuleFor(c => c.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 15)
                .Must(ValidationMethods.IsValidName).WithMessage("{PropertyName} should be only Alphabetic Characters");

            RuleFor(c => c.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 15)
                .Must(ValidationMethods.IsValidName).WithMessage("{PropertyName} should be only Alphabetic Characters");
        }
    }
}
