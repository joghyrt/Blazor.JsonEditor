using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;

namespace Blazor.JsonEditor.Demo.Component
{
    public partial class JsonItemCustomView
    {
        [Parameter]
        public KeyValuePair<string, JsonNode?> JsonItem { get; set; }
    }
}

