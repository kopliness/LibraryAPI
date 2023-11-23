using FluentValidation;

namespace Library.Business.Validation;

public class IsbnValidator : AbstractValidator<string>
{
    public IsbnValidator()
    {
        RuleFor(x => x)
            .Length(13).WithMessage("Incorrect isbn length")
            .Must(x => x.All(char.IsDigit))
            .WithMessage("The ISBN must contain only numbers");
    }
}