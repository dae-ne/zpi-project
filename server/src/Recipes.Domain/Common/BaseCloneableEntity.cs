using System.Text.Json;

namespace Recipes.Domain.Common;

public abstract class BaseCloneableEntity : BaseCloneableEntity<int>;

public abstract class BaseCloneableEntity<TKey> : BaseEntity<TKey>, ICloneable
    where TKey : IEquatable<TKey>
{
    public object Clone()
    {
        var json = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize(json, GetType())!;
    }
}
