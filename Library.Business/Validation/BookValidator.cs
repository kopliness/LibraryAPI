using System.Text.RegularExpressions;
using FluentValidation;
using Library.Business.Dto;

namespace Library.Business.Validation;

public class BookValidator : AbstractValidator<BookCreateDto>
{
    public BookValidator()
    {
        RuleFor(x => x.Isbn)
            .NotEmpty().WithMessage("ISBN is required")
            .Length(13).WithMessage("Incorrect ISBN format");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(250).WithMessage("The title is too long")
            .MinimumLength(5).WithMessage("The title is too short");

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre is required")
            .MaximumLength(20).WithMessage("The genre is too long")
            .MinimumLength(5).WithMessage("The genre is too short")
            .Must(BeAValidText).WithMessage("Incorrect genre");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("The description is too long")
            .MinimumLength(10).WithMessage("The description is too short")
            .Must(NotContainBannedWords).WithMessage("Bad words in description");

        RuleFor(x => x.Authors)
            .NotEmpty().WithMessage("Authors is required");

        RuleFor(x => x.Authors)
            .NotEmpty().WithMessage("Authors is required")
            .Must(BeUnique).WithMessage("Author IDs must be unique");

        RuleForEach(x => x.Authors)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty");
        ;
    }

    private bool BeUnique(List<Guid> authorIds)
    {
        var uniqueAuthorIds = new HashSet<Guid>(authorIds);
        return uniqueAuthorIds.Count == authorIds.Count;
    }

    private bool BeAValidText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        var regex = new Regex(@"^[A-Za-z\s]+$");

        return regex.IsMatch(text);
    }

    private bool BeAValidPastDate(DateTime? date)
    {
        if (!date.HasValue) return false;

        return date.Value < DateTime.Now;
    }

    private bool NotContainBannedWords(string text)
    {
        var bannedWords = new List<string> { "banned", "bad", "word" };

        return !bannedWords.Any(x => text.Contains(x));
    }
}