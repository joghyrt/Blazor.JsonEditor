using Microsoft.AspNetCore.Components;
using System.Text.Json.Nodes;
using System.Text.Json;
using Blazor.JsonEditor.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Blazor.JsonEditor.Component
{
    public partial class JsonItemEditor
    {
        [Parameter] public JsonObject? JsonObject { get; set; }

        [Parameter] public EventCallback<JsonObject> JsonObjectChanged { get; set; }

        [Parameter] public Dictionary<string, string>? KeyValues { get; set; }

        [Parameter] public string? EditingPropertyName { get; set; }

        private bool IsAddingOrEditing;

        private bool IsEdit
        {
            get
            {
                return !string.IsNullOrEmpty(EditingPropertyName);
            }
        }

        private string? ValidationMessage;

        private JsonItem? JsonItem { get; set; }

        private void AddOrEdit()
        {
            IsAddingOrEditing = !IsAddingOrEditing;
            ValidationMessage = null;
            StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            JsonItem = new JsonItem();
            if (EditingPropertyName == null)
            {
                JsonItem.ValueKind = JsonValueKind.Undefined;
                return;
            }

            this.ProcessEditMode();
        }

        private void ProcessEditMode()
        {
            JsonItem.PropertyName = EditingPropertyName;

            var jsonObjectValue = JsonObject?.FirstOrDefault(x => x.Key.Equals(EditingPropertyName)).Value;

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
                    IsAddingOrEditing = false;
                    return;
                }
                
                if (!this.IsEdit)
                {
                    this.AddNode();
                }
                else
                {
                    this.EditNode();
                }

                await JsonObjectChanged.InvokeAsync(JsonObject);
                JsonItem = null;
                IsAddingOrEditing = false;
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

            switch (JsonItem.ValueKind)
            {
                case JsonValueKind.String when !string.IsNullOrWhiteSpace(JsonItem.Value):
                    JsonObject.Add(JsonItem.PropertyName, JsonItem.Value);
                    break;
                case JsonValueKind.Number when JsonItem.NumericValue != null:
                    JsonObject.Add(JsonItem.PropertyName, JsonItem.NumericValue.Value);
                    break;
                case JsonValueKind.Undefined:
                    JsonObject.Add(JsonItem.PropertyName, null);
                    break;
                case JsonValueKind.Object:
                    JsonObject.Add(JsonItem.PropertyName, new JsonObject());
                    break;
                case JsonValueKind.Array when !string.IsNullOrEmpty(JsonItem.Value):
                {
                    List<JsonNode> nodeArray = JsonItem.Value.Split(',').Select(x => JsonNode.Parse(x)).ToList();

                    JsonObject.Add(JsonItem.PropertyName, new JsonArray(nodeArray.ToArray()));
                    break;
                }
                case JsonValueKind.True when JsonItem.Value != null:
                    JsonObject.Add(JsonItem.PropertyName, Convert.ToBoolean(JsonItem.Value));
                    break;
                case JsonValueKind.False or JsonValueKind.Null when
                    !string.IsNullOrEmpty(JsonItem.Value):
                {
                    if (!JsonItem.Value.StartsWith("{"))
                    {
                        JsonItem.Value = "{" + JsonItem.Value + "}";
                    }

                    var node = (JsonObject)JsonNode.Parse(JsonItem.Value);

                    var pr = node.Select(x => x.Key).ToList();
                    if (pr?.Count > 0)
                    {
                        foreach (var pn in pr)
                        {
                            var jsNode = node.FirstOrDefault(x => x.Key.Equals(pn)).Value;
                            JsonObject.Add(pn, jsNode.Root);
                        }
                    }

                    break;
                }
            }
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

            if (string.IsNullOrEmpty(EditingPropertyName))
            {
                return;
            }

            switch (JsonItem.ValueKind)
            {
                case JsonValueKind.String when !string.IsNullOrWhiteSpace(JsonItem.Value):
                    JsonObject[EditingPropertyName] = JsonItem.Value;
                    break;
                case JsonValueKind.Number when JsonItem.NumericValue != null:
                    JsonObject[EditingPropertyName] = JsonItem.NumericValue.Value;
                    break;
                case JsonValueKind.Undefined:
                    JsonObject[EditingPropertyName] = null;
                    break;
                case JsonValueKind.Object:
                    JsonObject[EditingPropertyName] = new JsonObject();
                    break;
                case JsonValueKind.Array when !string.IsNullOrEmpty(JsonItem.Value):
                {
                    List<JsonNode> nodeArray = JsonItem.Value.Split(',').Select(x => JsonNode.Parse(x)).ToList();

                    JsonObject[EditingPropertyName] = new JsonArray(nodeArray.ToArray());
                    break;
                }
                case JsonValueKind.True when JsonItem.Value != null:
                    JsonObject[EditingPropertyName] = Convert.ToBoolean(JsonItem.Value);
                    break;
            }
        }
    }
}