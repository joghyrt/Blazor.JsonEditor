using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;

namespace Blazor.JsonEditor.Component.Viewer
{
    public partial class JsonItemView
    {
        [Parameter]
        public KeyValuePair<string, JsonNode?> JsonItem { get; set; }
    }
}

