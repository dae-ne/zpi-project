using FluentValidation;

namespace Dietly.Application.Images.Queries.GetAvatar;

internal sealed class GetAvatarQueryValidator : AbstractValidator<GetAvatarQuery>
{
    public GetAvatarQueryValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name must not be empty.");
    }
}
