using FluentValidation;
using LuxuryHub.Application.Requests;

namespace LuxuryHub.Application.Validators;

public class CreatePropertyRequestValidator : AbstractValidator<CreatePropertyRequest>
{
    public CreatePropertyRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");

        RuleFor(x => x.CodeInternal)
            .NotEmpty().WithMessage("Code internal is required")
            .MaximumLength(50).WithMessage("Code internal must not exceed 50 characters");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, 2100).WithMessage("Year must be between 1900 and 2100");

        RuleFor(x => x.IdOwner)
            .NotEmpty().WithMessage("Owner ID is required");
    }
}
