using Application.DataTransferObjects;
using FluentValidation;

namespace Application.Validators
{
    public class CreateClientValidator : AbstractValidator<CreateClientDTO>
    {
        public CreateClientValidator()
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

            RuleFor(c => c.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(5)
                .Matches("^[A-Z]{2}[0-9]{3}$").WithMessage("{PropertyName} should be 2 Uppercase characters followed by 3 Digits");
        }
    }
}
