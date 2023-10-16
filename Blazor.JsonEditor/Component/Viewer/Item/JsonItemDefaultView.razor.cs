using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;

namespace Blazor.JsonEditor.Component.Viewer.Item
{
    public partial class JsonItemDefaultView
    {
        [Parameter]
        public KeyValuePair<string, JsonNode?> JsonItem { get; set; }
    }
}

