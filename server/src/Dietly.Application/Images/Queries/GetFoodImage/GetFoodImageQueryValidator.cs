using FluentValidation;

namespace Dietly.Application.Images.Queries.GetFoodImage;

internal sealed class GetFoodImageQueryValidator : AbstractValidator<GetFoodImageQuery>
{
    public GetFoodImageQueryValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name must not be empty.");
    }
}
