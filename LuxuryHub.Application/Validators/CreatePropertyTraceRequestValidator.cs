using FluentValidation;
using LuxuryHub.Application.Requests;

namespace LuxuryHub.Application.Validators;

public class CreatePropertyTraceRequestValidator : AbstractValidator<CreatePropertyTraceRequest>
{
    public CreatePropertyTraceRequestValidator()
    {
        RuleFor(x => x.DateSale)
            .NotEmpty().WithMessage("Date sale is required")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date sale cannot be in the future");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0).WithMessage("Value must be greater than or equal to 0");

        RuleFor(x => x.Tax)
            .GreaterThanOrEqualTo(0).WithMessage("Tax must be greater than or equal to 0");

        RuleFor(x => x.IdProperty)
            .NotEmpty().WithMessage("Property ID is required");
    }
}
