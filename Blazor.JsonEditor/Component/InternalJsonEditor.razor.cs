using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazor.JsonEditor.Component
{
    public partial class InternalJsonEditor
    {
        [Parameter]
        public string? Value { get; set; }

        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }

        [Parameter]
        public Dictionary<string, string>? KeyValues { get; set; }

        private JsonObject? Json { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Value != null)
            {
                Json = JsonNode.Parse(Value) as JsonObject;
            }
            await base.OnParametersSetAsync();
        }

        private void ValueUpdated(JsonObject jsonObject)
        {
            this.Json = jsonObject;
            this.ValueChanged.InvokeAsync(jsonObject.ToJsonString());
            this.StateHasChanged();
        }

        private void JsonObjectUpdated(string prop, string value)
        {
            if (this.Json == null)
            {
                return;
            }
            
            this.Json[prop] = JsonNode.Parse(value);
            this.ValueChanged.InvokeAsync(Json.ToJsonString());
            this.StateHasChanged();
        }

        private void RemoveValue(string prop)
        {
            if (this.Json == null)
            {
                return;
            }
            
            this.Json.Remove(prop);
            this.ValueChanged.InvokeAsync(Json.ToJsonString());
            this.StateHasChanged();
        }
    }
}
