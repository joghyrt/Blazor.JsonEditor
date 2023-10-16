using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;

namespace Blazor.JsonEditor.Component.Viewer.Item
{
    public partial class JsonItemView
    {
        [Parameter]
        public Type? Component { get; set; }
        
        [Parameter]
        public KeyValuePair<string, JsonNode?> JsonItem { get; set; }
        
        private Dictionary<string, object> DynamicComponentParameters = new();
        
        protected override async Task OnParametersSetAsync()
        {
            if (Component != null)
            {
                AddOrUpdateParameter("JsonItem", JsonItem);
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

