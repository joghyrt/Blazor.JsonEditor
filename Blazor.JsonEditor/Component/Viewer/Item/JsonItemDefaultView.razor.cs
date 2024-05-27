using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;

namespace Blazor.JsonEditor.Component.Viewer.Item
{
    public partial class JsonItemDefaultView
    {
        [Parameter]
        public KeyValuePair<string, JsonNode?> JsonItem { get; set; }
        
        [Parameter]
        public JsonObject? JsonObject { get; set; }
        
        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }

        [Parameter]
        public Dictionary<string, string>? KeyValues { get; set; }
        
        [Parameter] 
        public bool AllowEdit { get; set; } = true;
        
        [Parameter] 
        public Type? CustomEditor { get; set; }
        
        [Parameter] 
        public Type? CustomItemView { get; set; }
        
        [Parameter] 
        public Type? CustomObjectView { get; set; }
        
        private void JsonObjectUpdated(int index, string value)
        {
            var jsonValue = JsonItem.Value as JsonArray;

            jsonValue[index] = JsonNode.Parse(value);
            
            JsonItem = new KeyValuePair<string, JsonNode?>(JsonItem.Key, jsonValue);

            ValueChanged.InvokeAsync(JsonItem.Value.ToJsonString());
            
            // // this.JsonObject[prop] = JsonNode.Parse(value);
            // // this.JsonObjectChanged.InvokeAsync(JsonObject.ToJsonString());
            // this.StateHasChanged();
        }
    }
}

