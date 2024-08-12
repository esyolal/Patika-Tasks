using FluentValidation;
using BookStore.Dto;
namespace BookStore.Validator;
public class UpdateAuthorDtoValidator : AbstractValidator<UpdateAuthorDto>
{
    public UpdateAuthorDtoValidator()
    {
        RuleFor(a => a.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(a => a.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(a => a.DateOfBirth).NotEmpty().WithMessage("Date of birth is required.");
    }
}
