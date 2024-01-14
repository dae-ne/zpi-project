using FluentValidation;

namespace Dietly.Application.Images.Commands.RemoveAvatar;

internal sealed class RemoveAvatarCommandValidator : AbstractValidator<RemoveAvatarCommand>
{
    public RemoveAvatarCommandValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name must not be empty.");
    }
}
