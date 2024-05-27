using System.Text.Json;
using System.Text.Json.Nodes;
using Blazor.JsonEditor.Model;

namespace Blazor.JsonEditor.Helper
{
    public static class JsonHelper
    {
        public static bool IsInJsonValueKind(KeyValuePair<string, JsonNode?> jsonObjectValue,
            IEnumerable<JsonValueKind> jsonValueKinds)
        {
            if (jsonObjectValue.Value == null)
            {
                return false;
            }

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonObjectValue.Value.ToJsonString());

            return jsonValueKinds.Any(x => jsonElement.ValueKind.Equals(x));
        }

        public static bool IsObjectValueKind(KeyValuePair<string, JsonNode?> jsonObjectValue)
        {
            if (jsonObjectValue.Value == null)
            {
                return false;
            }

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonObjectValue.Value.ToJsonString());

            return jsonElement.ValueKind.Equals(JsonValueKind.Object);
        }

        public static void EditNodeValue(JsonObject jsonObject, JsonItem jsonItem, string editPropertyName)
        {
            switch (jsonItem.ValueKind)
            {
                case JsonValueKind.String when !string.IsNullOrWhiteSpace(jsonItem.Value):
                    jsonObject[editPropertyName] = jsonItem.Value;
                    break;
                case JsonValueKind.Number when jsonItem.NumericValue != null:
                    jsonObject[editPropertyName] = jsonItem.NumericValue.Value;
                    break;
                case JsonValueKind.Undefined:
                    jsonObject[editPropertyName] = null;
                    break;
                case JsonValueKind.Object:
                    jsonObject[editPropertyName] = new JsonObject();
                    break;
                case JsonValueKind.Array when !jsonItem.ArrayType.Equals(JsonValueKind.Object):
                {
                    List<JsonNode> nodeArray = jsonItem.Value.Split(',').Select(x => JsonNode.Parse(x)).ToList();

                    jsonObject[editPropertyName] = new JsonArray(nodeArray.ToArray());
                    break;
                }
                case JsonValueKind.Array when jsonItem.ArrayType.Equals(JsonValueKind.Object): 
                {
                    var document = JsonDocument.Parse($"[{jsonItem.Value}]"); 

                    var jsonArray = new JsonArray();
                    foreach (var element in document.RootElement.EnumerateArray())
                    {
                        jsonArray.Add(JsonNode.Parse(element.GetRawText()));
                    }

                    jsonObject[editPropertyName] = jsonArray;
                    break;
                }
                case JsonValueKind.True when jsonItem.Value != null:
                    jsonObject[editPropertyName] = Convert.ToBoolean(jsonItem.Value);
                    break;
                case JsonValueKind.False:
                    jsonObject[editPropertyName] = Convert.ToBoolean(jsonItem.Value);
                    break;
                case JsonValueKind.Null:
                    jsonObject[editPropertyName] = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void AddNodeValue(JsonObject jsonObject, JsonItem jsonItem)
        {
            switch (jsonItem.ValueKind)
            {
                case JsonValueKind.String when !string.IsNullOrWhiteSpace(jsonItem.Value):
                    jsonObject.Add(jsonItem.PropertyName, jsonItem.Value);
                    break;
                case JsonValueKind.Number when jsonItem.NumericValue != null:
                    jsonObject.Add(jsonItem.PropertyName, jsonItem.NumericValue.Value);
                    break;
                case JsonValueKind.Undefined:
                    jsonObject.Add(jsonItem.PropertyName, null);
                    break;
                case JsonValueKind.Object:
                    jsonObject.Add(jsonItem.PropertyName, new JsonObject());
                    break;
                case JsonValueKind.Array when !jsonItem.ArrayType.Equals(JsonValueKind.Object):
                {
                    List<JsonNode> nodeArray = jsonItem.Value.Split(',').Select(x => JsonNode.Parse(x)).ToList();

                    jsonObject.Add(jsonItem.PropertyName, new JsonArray(nodeArray.ToArray()));
                    break;
                }
                case JsonValueKind.Array when jsonItem.ArrayType.Equals(JsonValueKind.Object): 
                {
                    var document = JsonDocument.Parse($"[{jsonItem.Value}]"); 

                    var jsonArray = new JsonArray();
                    foreach (var element in document.RootElement.EnumerateArray())
                    {
                        jsonArray.Add(JsonNode.Parse(element.GetRawText()));
                    }

                    jsonObject.Add(jsonItem.PropertyName, jsonArray); 
                    break;
                }
                case JsonValueKind.True when jsonItem.Value != null:
                    jsonObject.Add(jsonItem.PropertyName, Convert.ToBoolean(jsonItem.Value));
                    break;
                case JsonValueKind.False or JsonValueKind.Null when !string.IsNullOrEmpty(jsonItem.Value):
                {
                    if (!jsonItem.Value.StartsWith("{"))
                    {
                        jsonItem.Value = "{" + jsonItem.Value + "}";
                    }

                    var node = (JsonObject)JsonNode.Parse(jsonItem.Value);

                    var pr = node.Select(x => x.Key).ToList();
                    if (pr?.Count > 0)
                    {
                        foreach (var pn in pr)
                        {
                            var jsNode = node.FirstOrDefault(x => x.Key.Equals(pn)).Value;
                            jsonObject.Add(pn, jsNode.Root);
                        }
                    }

                    break;
                }
            }
        }
        
        internal static bool IsShowComma(JsonObject json, KeyValuePair<string, JsonNode?> jsonItem)
        {
            if (json == null)
            {
                return false;
            }

            return !json.LastOrDefault().Key.Equals(jsonItem.Key);
        }
    }
}