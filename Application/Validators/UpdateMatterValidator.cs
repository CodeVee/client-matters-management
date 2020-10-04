using Application.DataTransferObjects;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateMatterValidator : AbstractValidator<UpdateMatterDTO>
    {
        public UpdateMatterValidator()
        {
            RuleFor(c => c.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(10, 100);

            RuleFor(c => c.Amount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
