using FluentValidation;

namespace Dietly.Application.Plans.Queries.GetPlan;

internal sealed class GetPlanQueryValidator : AbstractValidator<GetPlanQuery>
{
    public GetPlanQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");
    }
}
