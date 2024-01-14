using Dietly.Domain.Entities;
using Dietly.Domain.Extensions;

namespace Dietly.Domain.UnitTests;

// TODO: more tests
public class MealsExtensionsTests
{
    [Fact]
    public void ToDayPlan_Returns_CorrectSingleDayPlan()
    {
        // Arrange
        var meals = new List<Meal>
        {
            // TODO: Add more meals as needed for different scenarios
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 300 }, Date = DateTime.Now.Date, Completed = true },
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 400 }, Date = DateTime.Now.Date, Completed = false }
        };

        var day = DateOnly.FromDateTime(DateTime.Now.Date);
        var userId = 1;

        // Act
        var result = meals.ToDayPlan(day, userId);

        // Assert
        // TODO: Add more assertions based on the expected behavior of ToDayPlan method
        Assert.NotNull(result);
        Assert.Equal(day, result.Date);
    }

    [Fact]
    public void ToDayPlans_Returns_CorrectDayPlans()
    {
        // Arrange
        var meals = new List<Meal>
        {
            // TODO: Add more meals as needed for different scenarios
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 300 }, Date = DateTime.Now.Date, Completed = true },
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 400 }, Date = DateTime.Now.Date.AddDays(-1), Completed = true },
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 500 }, Date = DateTime.Now.Date.AddDays(1), Completed = true }
        };

        var from = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1));
        var to = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(1));
        var userId = 1;

        // Act
        var result = meals.ToDayPlans(from, to, userId);

        // Assert
        // TODO: Add assertions to check the correctness of the returned DayPlans
        Assert.NotNull(result);
    }
}
