using FluentValidation;
using LuxuryHub.Application.Requests;

namespace LuxuryHub.Application.Validators;

public class CreateOwnerRequestValidator : AbstractValidator<CreateOwnerRequest>
{
    public CreateOwnerRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters");

        RuleFor(x => x.Photo)
            .NotEmpty().WithMessage("Photo is required")
            .MaximumLength(500).WithMessage("Photo URL must not exceed 500 characters");

        RuleFor(x => x.Birthday)
            .NotEmpty().WithMessage("Birthday is required")
            .LessThan(DateTime.Now).WithMessage("Birthday must be in the past");
    }
}
