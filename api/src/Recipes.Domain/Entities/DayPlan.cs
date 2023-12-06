﻿using Recipes.Domain.Common;

namespace Recipes.Domain.Entities;

public class DayPlan : BaseCloneableEntity
{
    public IEnumerable<Meal> Meals { get; set; } = null!;
    
    public DateOnly Date { get; set; }
    
    public int TotalCalories { get; set; }
    
    public int ConsumedCalories { get; set; }
}