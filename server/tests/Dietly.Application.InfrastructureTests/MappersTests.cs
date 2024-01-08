namespace Dietly.Application.InfrastructureTests;

public class MappersTests
{
    [Fact]
    // all mappers should be internal
    // mappers are static classes with extension methods and their names end with Mapper
    public void AllMappersShouldNotBePublic()
    {
        var allMappers = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => t.IsAbstract && t.Name.EndsWith("Mapper"))
            .ToList();

        var publicMappers = allMappers.Where(m => m.IsPublic).ToList();

        Assert.Empty(publicMappers);
    }
}
