using System.Text.Json;
using System.Text.Json.Nodes;

namespace Blazor.JsonEditor.Helper
{
    internal static class JsonHelper
    {
        internal static bool IsInJsonValueKind(KeyValuePair<string, JsonNode?> jsonObjectValue, IEnumerable<JsonValueKind> jsonValueKinds)
        {
            if (jsonObjectValue.Value == null)
            {
                return false;
            }

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonObjectValue.Value.ToJsonString());

            return jsonValueKinds.Any(x => jsonElement.ValueKind.Equals(x));
        }
        
        internal static bool IsObjectValueKind(KeyValuePair<string, JsonNode?> jsonObjectValue)
        {
            if (jsonObjectValue.Value == null)
            {
                return false;
            }

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonObjectValue.Value.ToJsonString());

            return jsonElement.ValueKind.Equals(JsonValueKind.Object);
        }
    }
}