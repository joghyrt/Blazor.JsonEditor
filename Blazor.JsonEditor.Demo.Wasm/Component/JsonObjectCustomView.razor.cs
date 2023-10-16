using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;

namespace Blazor.JsonEditor.Demo.Wasm.Component
{
    public partial class JsonObjectCustomView
    {
        
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
        
    }
}

