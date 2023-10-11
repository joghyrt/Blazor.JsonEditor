using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazor.JsonEditor.Component
{
    public partial class JsonEditor
    {
        [Parameter] public Dictionary<string, string>? KeyValues { get; set; }
        [Parameter] public Expression<Func<string?>>? ValidationFor { get; set; }
        [Parameter] public string? FieldName { get; set; }

        private JsonObject? Json { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Value != null)
            {
                Json = JsonNode.Parse(Value) as JsonObject;
            }

            await base.OnParametersSetAsync();
        }

        protected override bool TryParseValueFromString(string value, out string? result, out string? validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }

        private void ValueUpdated(JsonObject jsonObject)
        {
            Json = jsonObject;

            TryParseValueFromString(jsonObject.ToJsonString(), out string? JsonObjectString, out string? vm);
            ValueChanged.InvokeAsync(JsonObjectString);
            EditContext.NotifyFieldChanged(new FieldIdentifier(EditContext.Model, FieldName));

            StateHasChanged();
        }

        private void JsonObjectUpdated(string prop, string value)
        {
            if (Json != null)
            {
                Json[prop] = JsonNode.Parse(value);
                TryParseValueFromString(Json.ToJsonString(), out string? JsonObjectString, out string? vm);
                ValueChanged.InvokeAsync(JsonObjectString);
                EditContext.NotifyFieldChanged(new FieldIdentifier(EditContext.Model, FieldName));
                StateHasChanged();
            }
        }

        private void RemoveValue(string prop)
        {
            if (Json != null)
            {
                Json.Remove(prop);
                TryParseValueFromString(Json.ToJsonString(), out string? JsonObjectString, out string? vm);
                ValueChanged.InvokeAsync(JsonObjectString);
                EditContext.NotifyFieldChanged(new FieldIdentifier(EditContext.Model, FieldName));
                StateHasChanged();
            }
        }

        private async Task CopyToClipboard()
        {
            await JS.InvokeAsync<string?>("CopyToClipboard", new object[] { Value });
        }
    }
}
