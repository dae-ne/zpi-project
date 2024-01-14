using FluentValidation;

namespace Dietly.Application.Users.Commands.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("User name is required.")
            .MaximumLength(100)
            .WithMessage("User name must not exceed 100 characters.");
    }
}
