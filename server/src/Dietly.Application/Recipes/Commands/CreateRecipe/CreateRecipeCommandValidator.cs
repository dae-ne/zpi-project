using FluentValidation;

namespace Dietly.Application.Recipes.Commands.CreateRecipe;

internal sealed class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
{
public CreateRecipeCommandValidator()
    {
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
