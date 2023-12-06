﻿using Recipes.Domain.Common;

namespace Recipes.Domain.Entities;

public class Tag : BaseCloneableEntity
{
    public int UserId { get; set; }
    
    public string Name { get; set; } = null!;
    
    // public List<Recipe> Recipes { get; set; } = new();
}
