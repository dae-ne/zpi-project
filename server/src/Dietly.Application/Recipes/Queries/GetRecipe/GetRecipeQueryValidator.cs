using FluentValidation;

namespace Dietly.Application.Recipes.Queries.GetRecipe;

internal sealed class GetRecipeQueryValidator : AbstractValidator<GetRecipeQuery>
{
    public GetRecipeQueryValidator()
    {
        RuleFor(x => x.RecipeId)
            .GreaterThan(0)
            .WithMessage("Recipe id must be greater than 0.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");
    }
}
