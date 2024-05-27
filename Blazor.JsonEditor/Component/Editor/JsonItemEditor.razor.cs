using Microsoft.AspNetCore.Components;
using System.Text.Json.Nodes;

namespace Blazor.JsonEditor.Component.Editor
{
    public partial class JsonItemEditor
    {
        [Parameter]
        public Type? Component { get; set; }
        
        [Parameter] 
        public JsonObject? JsonObject { get; set; }

        [Parameter] 
        public EventCallback<JsonObject> JsonObjectChanged { get; set; }

        [Parameter] 
        public Dictionary<string, string>? KeyValues { get; set; }

        [Parameter]
        public KeyValuePair<string, JsonNode?>? JsonItemToEdit { get; set; }

        private Dictionary<string, object> DynamicComponentParameters = new();
        
        protected override async Task OnParametersSetAsync()
        {
            if (Component != null)
            {
                AddOrUpdateParameter("JsonObject", JsonObject);
                AddOrUpdateParameter("JsonObjectChanged", JsonObjectChanged);
                AddOrUpdateParameter("KeyValues", KeyValues);
                AddOrUpdateParameter("JsonItemToEdit", JsonItemToEdit);
            }
        }

        private void AddOrUpdateParameter(string key, object value)
        {
            if (DynamicComponentParameters.TryAdd(key, value))
            {
                return;
            }

            DynamicComponentParameters.Remove(key);
            DynamicComponentParameters.TryAdd(key, value);
        }
    }
}