using FluentValidation;

namespace Dietly.Application.Recipes.Commands.RemoveRecipe;

internal sealed class RemoveRecipeCommandValidator : AbstractValidator<RemoveRecipeCommand>
{
    public RemoveRecipeCommandValidator()
    {
        RuleFor(x => x.RecipeId)
            .GreaterThan(0)
            .WithMessage("Recipe id must be greater than 0.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");
    }
}
