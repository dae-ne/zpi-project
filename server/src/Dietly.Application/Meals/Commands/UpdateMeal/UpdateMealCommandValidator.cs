using FluentValidation;

namespace Dietly.Application.Meals.Commands.UpdateMeal;

internal sealed class UpdateMealCommandValidator : AbstractValidator<UpdateMealCommand>
{
    public UpdateMealCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");

        RuleFor(x => x.MealId)
            .GreaterThan(0)
            .WithMessage("Meal id must be greater than 0.");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date must not be empty.");
    }
}
