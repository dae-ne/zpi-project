using FluentValidation;

namespace Dietly.Application.Meals.Queries.GetMeals;

internal sealed class GetMealsQueryValidator : AbstractValidator<GetMealsQuery>
{
    public GetMealsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0.");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("Start date cannot be later than end date.");
    }
}
