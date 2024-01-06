using Recipes.Domain.Entities;
using Recipes.Domain.Extensions;

namespace Recipes.Domain.UnitTests;

// TODO: more tests

public class MealsExtensionsTests
{
    [Fact]
    public void ToDayPlan_Returns_CorrectSingleDayPlan()
    {
        // Arrange
        var meals = new List<Meal>
        {
            // Create sample Meal objects for testing
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 300 }, Date = DateTime.Now.Date, Completed = true },
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 400 }, Date = DateTime.Now.Date, Completed = false }
            // Add more meals as needed for different scenarios
        };

        var day = DateOnly.FromDateTime(DateTime.Now.Date);
        var userId = 1;

        // Act
        var result = meals.ToDayPlan(day, userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(day, result.Date);
        // Add more assertions based on the expected behavior of ToDayPlan method
    }

    [Fact]
    public void ToDayPlans_Returns_CorrectDayPlans()
    {
        // Arrange
        var meals = new List<Meal>
        {
            // Create sample Meal objects for testing
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 300 }, Date = DateTime.Now.Date, Completed = true },
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 400 }, Date = DateTime.Now.Date.AddDays(-1), Completed = true },
            new Meal { Recipe = new Recipe { UserId = 1, Calories = 500 }, Date = DateTime.Now.Date.AddDays(1), Completed = true }
            // Add more meals as needed for different scenarios
        };

        var from = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1));
        var to = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(1));
        var userId = 1;

        // Act
        var result = meals.ToDayPlans(from, to, userId);

        // Assert
        Assert.NotNull(result);
        // Add assertions to check the correctness of the returned DayPlans
    }
}
