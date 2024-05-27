using Blazor.JsonEditor.Demo.Wasm.Model;

namespace Blazor.JsonEditor.Demo.Wasm.Pages
{
    public partial class Index
    {
        private IndexModel DemoJson { get; set; } = new()
        {
            Json =
                "{\"Nullable\": null, \"String\": \"random\", \"Number\": 1, \"Array\": [1,2,3], \"True\": true, \"False\": false, \"Object\": { \"Nullable\": null, \"String\": \"random\", \"Number\": 1, \"Array\": [1,2,3], \"True\": true, \"False\": false, \"ObjectsArray\": [{ \"Name\":\"First Object\", \"Property\": 1}, {\"Name\":\"Second Object\", \"Property\": 1}]}}" };
    }
}
