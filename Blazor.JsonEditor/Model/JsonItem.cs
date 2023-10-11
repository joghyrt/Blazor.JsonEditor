using Blazor.JsonEditor.Attribute;
using System.Text.Json;

namespace Blazor.JsonEditor.Model
{
    internal class JsonItem
    {
        [RequiredIf(nameof(JsonItem.ValueKind), JsonValueKind.Undefined, JsonValueKind.Number, JsonValueKind.String,
            JsonValueKind.Object, JsonValueKind.Array, JsonValueKind.True)]
        public string? PropertyName { get; set; }

        [RequiredIf(nameof(JsonItem.ValueKind), JsonValueKind.String, JsonValueKind.Array, JsonValueKind.False,
            JsonValueKind.Null, JsonValueKind.True)]
        public string? Value { get; set; }

        [RequiredIf(nameof(JsonItem.ValueKind), JsonValueKind.Number)]
        public double? NumericValue { get; set; }

        public JsonValueKind ValueKind { get; set; }
    }
}
