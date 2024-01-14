using FluentValidation;

namespace Dietly.Application.Meals.Commands.RemoveMeal;

internal sealed class RemoveMealCommandValidator : AbstractValidator<RemoveMealCommand>
{
    public RemoveMealCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");

        RuleFor(x => x.MealId)
            .GreaterThan(0)
            .WithMessage("Meal id must be greater than 0.");
    }
}
