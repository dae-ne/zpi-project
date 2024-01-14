﻿using FluentValidation;

namespace Dietly.Application.Images.Commands.AddFoodImage;

internal sealed class AddFoodImageCommandValidator : AbstractValidator<AddFoodImageCommand>
{
    public AddFoodImageCommandValidator()
    {
        RuleFor(x => x.File).
            NotNull().
            WithMessage("File must not be null.");

        RuleFor(x => x.File)
            .Must(x => x.Length > 0)
            .WithMessage("File must not be empty.");

        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name must not be empty.");
    }
}
