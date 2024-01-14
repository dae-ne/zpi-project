using FluentValidation;

namespace Dietly.Application.Meals.Commands.AddMeal;

internal sealed class AddMealCommandValidator : AbstractValidator<AddMealCommand>
{
    public AddMealCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");

        RuleFor(x => x.RecipeId)
            .GreaterThan(0)
            .WithMessage("Recipe id must be greater than 0.");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date must not be empty.");
    }
}
