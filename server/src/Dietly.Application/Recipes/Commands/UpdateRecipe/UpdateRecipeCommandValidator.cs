using FluentValidation;

namespace Dietly.Application.Recipes.Commands.UpdateRecipe;

internal sealed class UpdateRecipeCommandValidator : AbstractValidator<UpdateRecipeCommand>
{
    public UpdateRecipeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Recipe id must be greater than 0.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Recipe title is required.")
            .MaximumLength(100)
            .WithMessage("Recipe title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Recipe description is required.")
            .MaximumLength(500)
            .WithMessage("Recipe description must not exceed 500 characters.");

        RuleFor(x => x.Time)
            .GreaterThan(0)
            .WithMessage("Recipe time must be greater than 0.");

        RuleFor(x => x.Calories)
            .GreaterThan(0)
            .WithMessage("Recipe calories must be greater than 0.");

        RuleFor(x => x.Ingredients)
            .NotEmpty()
            .WithMessage("Recipe ingredients are required.");

        RuleForEach(x => x.Ingredients)
            .ChildRules(ingredient =>
            {
                ingredient.RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Ingredient name is required.")
                    .MaximumLength(100)
                    .WithMessage("Ingredient name must not exceed 100 characters.");
            });
    }
}
