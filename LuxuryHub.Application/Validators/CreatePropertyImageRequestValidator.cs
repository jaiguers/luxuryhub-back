using FluentValidation;
using LuxuryHub.Application.Requests;

namespace LuxuryHub.Application.Validators;

public class CreatePropertyImageRequestValidator : AbstractValidator<CreatePropertyImageRequest>
{
    public CreatePropertyImageRequestValidator()
    {
        RuleFor(x => x.IdProperty)
            .NotEmpty().WithMessage("Property ID is required");

        RuleFor(x => x.File)
            .NotEmpty().WithMessage("File is required")
            .MaximumLength(500).WithMessage("File URL must not exceed 500 characters");
    }
}
