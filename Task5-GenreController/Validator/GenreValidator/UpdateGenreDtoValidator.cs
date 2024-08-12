using BookStore.Dto;
using FluentValidation;
namespace BookStore.Validator;
public class UpdateGenreDtoValidator : AbstractValidator<UpdateGenreDto>
{
    public UpdateGenreDtoValidator()
    {
        RuleFor(g => g.Name).NotEmpty().WithMessage("Name is required.");
    }
}