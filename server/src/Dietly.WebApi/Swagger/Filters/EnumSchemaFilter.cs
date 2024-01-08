using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dietly.WebApi.Swagger.Filters;

internal sealed partial class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum)
        {
            return;
        }

        model.Enum.Clear();

        foreach (var enumName in Enum.GetNames(context.Type))
        {
            var memberInfo = context.Type.GetMember(enumName).FirstOrDefault(m => m.DeclaringType == context.Type);
            var enumMemberAttribute = memberInfo?
                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                .OfType<EnumMemberAttribute>()
                .FirstOrDefault();
            var label = enumMemberAttribute is null || string.IsNullOrWhiteSpace(enumMemberAttribute.Value)
                ? RegexExpression().Replace(enumName, m => $" {m.ToString().ToLower()}")
                : enumMemberAttribute.Value;
            model.Enum.Add(new OpenApiString(label));
        }
    }

    [GeneratedRegex("(\\B[A-Z])", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex RegexExpression();
}
