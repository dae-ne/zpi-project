using FluentValidation;

namespace Dietly.Application.Lists.Commands.SendEmailWithList;

internal sealed class SendEmailWithListCommandValidator : AbstractValidator<SendEmailWithListCommand>
{
    public SendEmailWithListCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");

        RuleFor(x => x.IngredientIds)
            .NotEmpty()
            .WithMessage("Ingredient ids must not be empty.");
    }
}
