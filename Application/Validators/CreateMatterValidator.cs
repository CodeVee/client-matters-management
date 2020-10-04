using Application.DataTransferObjects;
using FluentValidation;

namespace Application.Validators
{
    public class CreateMatterValidator : AbstractValidator<CreateMatterDTO>
    {
        public CreateMatterValidator()
        {
            RuleFor(c => c.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(10, 100);


            RuleFor(c => c.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(5)
                .Matches("^[A-Z]{2}[0-9]{3}$").WithMessage("{PropertyName} should be 2 Uppercase characters followed by 3 Digits");

            RuleFor(c => c.Amount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
