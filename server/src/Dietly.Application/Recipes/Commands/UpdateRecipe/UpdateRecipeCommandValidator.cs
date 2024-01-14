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
            .MaximumLength(200)
            .WithMessage("Recipe title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Recipe description is required.")
            .MaximumLength(2000)
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
                    .MaximumLength(200)
                    .WithMessage("Ingredient name must not exceed 100 characters.");
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

        RuleFor(x => x.Directions.ToList())
            .Must(directions => directions.Select(x => x.Order).Distinct().Count() == directions.Count)
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
