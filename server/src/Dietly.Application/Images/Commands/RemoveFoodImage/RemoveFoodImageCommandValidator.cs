using FluentValidation;

namespace Dietly.Application.Images.Commands.RemoveFoodImage;

internal sealed class RemoveFoodImageCommandValidator : AbstractValidator<RemoveFoodImageCommand>
{
    public RemoveFoodImageCommandValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name must not be empty.");
    }
}
