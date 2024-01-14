using FluentValidation;

namespace Dietly.Application.Recipes.Commands.CreateRecipe;

internal sealed class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
{
public CreateRecipeCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Recipe title is required.")
            .MaximumLength(200)
            .WithMessage("Recipe title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Recipe description is required.")
            .MaximumLength(2000)
            .WithMessage("Recipe description must not exceed 2000 characters.");

        RuleFor(x => x.Ingredients)
            .NotEmpty()
            .WithMessage("Recipe ingredients are required.");

        RuleForEach(x => x.Ingredients)
            .ChildRules(ingredient =>
            {
                ingredient.RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Ingredient name is required.")
                    .MaximumLength(200)
                    .WithMessage("Ingredient name must not exceed 200 characters.");
            });

        RuleForEach(x => x.Directions)
            .ChildRules(ingredient =>
            {
                ingredient.RuleFor(x => x.Description)
                    .NotEmpty()
                    .WithMessage("Direction description is required.")
                    .MaximumLength(2000)
                    .WithMessage("Direction description must not exceed 2000 characters.");
            });

        RuleFor(x => x.Directions)
            .Must(directions => directions.Select(x => x.Order).Distinct().Count() == directions.Count())
            .WithMessage("Directions must have unique order numbers.");

        RuleForEach(x => x.Tags)
            .ChildRules(tag =>
            {
                tag.RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Tag name is required.")
                    .MaximumLength(200)
                    .WithMessage("Tag name must not exceed 200 characters.");
            });
    }
}
