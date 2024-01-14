using FluentValidation;

namespace Dietly.Application.Lists.Queries.GetList;

internal sealed class GetListQueryValidator : AbstractValidator<GetListQuery>
{
    public GetListQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");

        RuleFor(x => x.From)
            .LessThanOrEqualTo(x => x.To)
            .WithMessage("From date cannot be later than to date.");
    }
}
