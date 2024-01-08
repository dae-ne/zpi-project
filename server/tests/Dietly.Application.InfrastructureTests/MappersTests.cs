namespace Dietly.Application.InfrastructureTests;

public class MappersTests
{
    [Fact]
    public void AllMappersShouldNotBePublic()
    {
        var allMappers = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => t.IsAbstract && t.Name.EndsWith("Mapper"))
            .ToList();

        var publicMappers = allMappers.Where(m => m.IsPublic).ToList();

        Assert.Empty(publicMappers);
    }
}
