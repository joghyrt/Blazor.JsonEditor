# Blazor.JsonEditor
Json Editor and Viewer for Blazor Server App and Wasm. Rewrited and working version of **[Blazoring.JsonEditor](https://github.com/vmakharashvili/Blazoring-JsonEditor)** 

### Demo:

[Click me to see the demo](https://652a73a4fdf1211e843f77fd--wondrous-genie-896870.netlify.app)

## Json Editor and Viewer tool

* To install the package run following command:

**`Install-Package Blazor.JsonEditor`**
or search **Blazor.JsonEditor** in Nuget gallery.

This will install Blazor.JsonEditor in your project. You also need to add in **_Imports.razor**:
```html
@using Blazor.JsonEditor.Component
```
Also, you need to add javascript file in _Host.cshtml ( Server app) or index.html ( Wasm)  file:

```html
<script src="_content/Blazor.JsonEditor/Blazor.JsonEditor.js"></script>
```
For icons suppor JsonEditor uses Font-Awesome icons library. You need to add link to _Host.cshtml ( Server app) or index.html ( Wasm) file:
```html
<link rel="stylesheet" href="https://use.fontawesome.com/releases/v6.4.2/css/all.css">
```

### Using in code to have an editor:

```html
<EditForm Model="DemoJson">
    <JsonEditor @bind-Value="DemoJson.Json" FieldName="@nameof(IndexModel.Json)" ValidationFor="@(() => DemoJson.Json)"></JsonEditor>
</EditForm>
```

### Using in code to have a viewer:

```html
<EditForm Model="DemoJson">
    <JsonEditor @bind-Value="DemoJson.Json" FieldName="@nameof(IndexModel.Json)" ValidationFor="@(() => DemoJson.Json)" AllowEdit="false"></JsonEditor>
</EditForm>
```

Blazor.JsonEditor doesn't work without EditForm. Also, validation is required.

## Customization

## Editor template:

You can customize and pass your own editor template as dynamic component. Editor template is everything that you see after property name and value like buttons and editor window itself. For this you need to set parameter CustomEditor. As an example **CustomEditor="typeof([JsonItemCustomEditor](https://github.com/joghyrt/Blazor.JsonEditor/tree/main/Blazor.JsonEditor.Demo/Component))"**

```html
<EditForm Model="DemoJson">
    <JsonEditor @bind-Value="DemoJson.Json" FieldName="@nameof(IndexModel.Json)" ValidationFor="@(() => DemoJson.Json)" CustomEditor="typeof(JsonItemCustomEditor)"></JsonEditor>
</EditForm>
```

In your custom component you need to have parameters:

```c#
//Main Json object
[Parameter] 
public JsonObject? JsonObject { get; set; }

//Event that you need to invoke on every object change
[Parameter] 
public EventCallback<JsonObject> JsonObjectChanged { get; set; }

//Values for dropdown
[Parameter] 
public Dictionary<string, string>? KeyValues { get; set; }

//Edit item in case of edit
[Parameter]
public KeyValuePair<string, JsonNode?>? JsonItemToEdit { get; set; }
```

Also on add and edit you need to pass data to **JsonHelper**. We have two methods for this: AddNodeValue and EditNodeValue

```c#
public static void AddNodeValue(JsonObject jsonObject, JsonItem jsonItem)

public static void EditNodeValue(JsonObject jsonObject, JsonItem jsonItem, string editPropertyName)
```

After Add/Edit/Delete do not forget to Invoke JsonObjectChanged

```c#
await JsonObjectChanged.InvokeAsync(JsonObject);
```

## Next steps

- Add availability to have customer template for item and object properties
- Refactor

## If you like it please support:

[Buy me a coffee](https://www.buymeacoffee.com/joghyrt)

<img width="120" alt="bmc_qr" src="https://github.com/joghyrt/Blazor.JsonEditor/assets/26901105/d914e23c-dc90-483b-98df-e977c6662672">






