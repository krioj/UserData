using System.Text.Json;

namespace UserData.Entitys.Extensions;

public static class EntityExtensions
{
    public static T? Copy<T>(this T itemToCopy) where T : IEntity
    {
        return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize<T>(itemToCopy));
    }
}

