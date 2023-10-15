using System.Text.Json;
using System.Text.Json.Nodes;
using Blazor.JsonEditor.Helper;
using Blazor.JsonEditor.Model;
using Microsoft.AspNetCore.Components;

namespace Blazor.JsonEditor.Demo.Component
{
    public partial class JsonItemCustomEditor
    {
        [Parameter] 
        public JsonObject? JsonObject { get; set; }

        [Parameter] 
        public EventCallback<JsonObject> JsonObjectChanged { get; set; }

        [Parameter] 
        public Dictionary<string, string>? KeyValues { get; set; }

        [Parameter]
        public KeyValuePair<string, JsonNode?>? JsonItemToEdit { get; set; }

        private bool IsShowEditor;

        private bool IsEditMode
        {
            get
            {
                return JsonItemToEdit != null;
            }
        }

        private string? ValidationMessage;

        private JsonItem? JsonItem { get; set; }

        private void ToggleEditor()
        {
            IsShowEditor = !IsShowEditor;
            ValidationMessage = null;
            StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            JsonItem = new JsonItem();
            if (JsonItemToEdit == null)
            {
                JsonItem.ValueKind = JsonValueKind.Undefined;
                return;
            }

            this.ProcessEditMode();
        }

        private void ProcessEditMode()
        {
            JsonItem.PropertyName = JsonItemToEdit.Value.Key;

            var jsonObjectValue = JsonObject?.FirstOrDefault(x => x.Key.Equals(JsonItemToEdit.Value.Key)).Value;

            var jsonElement = new JsonElement();

            if (jsonObjectValue != null)
            {
                jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonObjectValue.ToJsonString());
                JsonItem.ValueKind = jsonElement.ValueKind;
            }

            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.String:
                    JsonItem.Value = jsonElement.ToString();
                    break;
                case JsonValueKind.Number:
                    if (!double.TryParse(jsonElement.ToString(), out var numericValue))
                    {
                        throw new ArgumentException($"Not able to parse value {jsonElement.ToString()} to double.");
                    }

                    JsonItem.NumericValue = numericValue;
                    break;
                case JsonValueKind.True:
                    JsonItem.Value = "true";
                    break;
                case JsonValueKind.False:
                    JsonItem.Value = "false";
                    JsonItem.ValueKind = JsonValueKind.True;
                    break;
                case JsonValueKind.Array:
                {
                    var arrayValue = jsonElement.ToString();
                    JsonItem.Value = arrayValue.Substring(1, arrayValue.Length - 2);
                    break;
                }
                case JsonValueKind.Null:
                    JsonItem.ValueKind = JsonValueKind.Undefined;
                    break;
            }
        }

        private async Task SaveNodeAsync()
        {
            try
            {
                ValidationMessage = null;

                if (JsonItem == null)
                {
                    ToggleEditor();
                    return;
                }

                if (!this.IsEditMode)
                {
                    this.AddNode();
                }
                else
                {
                    this.EditNode();
                }

                await JsonObjectChanged.InvokeAsync(JsonObject);
                JsonItem = null;
                ToggleEditor();
            }
            catch (ArgumentException ex)
            {
                ValidationMessage = ex.Message;
            }
        }

        private void AddNode()
        {
            if (JsonItem == null || string.IsNullOrEmpty(JsonItem.PropertyName))
            {
                return;
            }

            if (JsonObject == null)
            {
                JsonObject = new JsonObject();
            }

            JsonHelper.AddNodeValue(JsonObject, JsonItem);
        }

        private void EditNode()
        {
            if (JsonObject == null)
            {
                return;
            }

            if (JsonItem == null)
            {
                return;
            }

            if (JsonItemToEdit == null)
            {
                return;
            }

            JsonHelper.EditNodeValue(JsonObject, JsonItem, JsonItemToEdit.Value.Key);
        }
        
        private async Task RemoveNode()
        {
            if (this.JsonObject == null)
            {
                return;
            }

            if (this.JsonItemToEdit == null)
            {
                return;
            }
            
            this.JsonObject.Remove(this.JsonItemToEdit.Value.Key);
            await JsonObjectChanged.InvokeAsync(JsonObject);
            ToggleEditor();
            this.StateHasChanged();
        }
    }
}