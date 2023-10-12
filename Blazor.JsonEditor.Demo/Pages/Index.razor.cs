using Blazor.JsonEditor.Demo.Model;

namespace Blazor.JsonEditor.Demo.Pages
{
    public partial class Index
    {
        private IndexModel DemoJson { get; set; } = new()
        {
            Json =
                "{\"Nullable\": null, \"String\": \"random\", \"Number\": 1, \"Array\": [1,2,3], \"True\": true, \"False\": false, \"Object\": { \"Nullable\": null, \"String\": \"random\", \"Number\": 1, \"Array\": [1,2,3], \"True\": true, \"False\": false } }"
        };
    }
}