using BookStore.Dto;
using FluentValidation;
namespace BookStore.Validator;
public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookDtoValidator()
    {
        RuleFor(b => b.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(b => b.AuthorId).GreaterThan(0).WithMessage("Valid AuthorId is required.");
        RuleFor(b => b.GenreId).GreaterThan(0).WithMessage("Valid GenreId is required.");
    }
}

