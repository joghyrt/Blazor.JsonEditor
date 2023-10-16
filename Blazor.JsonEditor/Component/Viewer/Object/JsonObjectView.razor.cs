using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;

namespace Blazor.JsonEditor.Component.Viewer.Object
{
    public partial class JsonObjectView
    {
        [Parameter]
        public Type? Component { get; set; }
        
        [Parameter]
        public KeyValuePair<string, JsonNode?> JsonItem { get; set; }
        
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
        
        private Dictionary<string, object> DynamicComponentParameters = new();
        
        protected override async Task OnParametersSetAsync()
        {
            if (Component != null)
            {
                AddOrUpdateParameter("JsonItem", JsonItem);
                AddOrUpdateParameter("ValueChanged", ValueChanged);
                AddOrUpdateParameter("KeyValues", KeyValues);
                AddOrUpdateParameter("AllowEdit", AllowEdit);
                AddOrUpdateParameter("CustomEditor", CustomEditor);
                AddOrUpdateParameter("CustomItemView", CustomItemView);
                AddOrUpdateParameter("CustomObjectView", CustomObjectView);
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
