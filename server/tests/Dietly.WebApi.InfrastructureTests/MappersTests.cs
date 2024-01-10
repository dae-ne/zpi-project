﻿using Dietly.WebApi.Infrastructure.Attributes;

namespace Dietly.WebApi.InfrastructureTests;

public class MappersTests
{
    [Fact]
    public void AllMappersShouldNotBePublic()
    {
        var allMappers = typeof(ApiEndpointAttribute).Assembly.GetTypes()
            .Where(t => t.IsAbstract && t.Name.EndsWith("Mapper"))
            .ToList();

        var publicMappers = allMappers.Where(m => m.IsPublic).ToList();

        Assert.Empty(publicMappers);
    }

    [Fact]
    public void AllMappersShouldHaveUnifiedName()
    {
        var allMappers = typeof(ApiEndpointAttribute).Assembly.GetTypes()
            .Where(t => t.IsAbstract && t.Name.EndsWith("Mapper"))
            .ToList();

        var mappersWithIncorrectName = allMappers.Where(m => m.Name != "Mapper")
            .ToList();

        Assert.Empty(mappersWithIncorrectName);
    }
}
