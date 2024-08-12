using BookStore.Dto;
using FluentValidation;
namespace BookStore.Validator;
public class CreateGenreDtoValidator : AbstractValidator<CreateGenreDto>
{
    public CreateGenreDtoValidator()
    {
        RuleFor(g => g.Name).NotEmpty().WithMessage("Name is required.");
    }
}