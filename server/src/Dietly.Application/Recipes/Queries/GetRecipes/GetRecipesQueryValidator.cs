using FluentValidation;

namespace Dietly.Application.Recipes.Queries.GetRecipes;

internal sealed class GetRecipesQueryValidator : AbstractValidator<GetRecipesQuery>
{
    public GetRecipesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");
    }
}
