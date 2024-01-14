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
            .MaximumLength(50)
            .WithMessage("User name must not exceed 50 characters.");
    }
}
