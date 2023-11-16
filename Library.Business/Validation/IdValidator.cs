using FluentValidation;

namespace Library.Business.Validation;

public class IdValidator : AbstractValidator<Guid>
{
    public IdValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty");
    }
}