# Blazor.JsonEditor

JSON Editor and Viewer ( with customization) for Blazor Server App and Wasm. Rewrited and working version of **[Blazoring.JsonEditor](https://github.com/vmakharashvili/Blazoring-JsonEditor)** 

## Demo:

[Click me to see the demo](https://66546b0fe74ef3008ca00fd0--cool-pasca-c1252e.netlify.app/)


## Json Editor and Viewer tool

* To install the package run the following command:

**`Install-Package Blazor.JsonEditor`**
or search **Blazor.JsonEditor** in Nuget gallery.

This will install Blazor.JsonEditor in your project. You also need to add in **_Imports.razor**:
```html
@using Blazor.JsonEditor.Component
```
Also, you need to add a javascript file in _Host.cshtml ( Server app) or index.html ( Wasm)  file:

```html
<script src="_content/Blazor.JsonEditor/Blazor.JsonEditor.js"></script>
```
For icon support, JsonEditor uses the Font-Awesome icons library. You need to add a link to _Host.cshtml ( Server app) or index.html ( Wasm) file:
```html
<link rel="stylesheet" href="https://use.fontawesome.com/releases/v6.4.2/css/all.css">
```

### Using in code to have an editor:

```html
<EditForm Model="DemoJson">
    <JsonEditor @bind-Value="DemoJson.Json"
                FieldName="@nameof(IndexModel.Json)"
                ValidationFor="@(() => DemoJson.Json)">
    </JsonEditor>
</EditForm>
```

### Using in code to have a viewer:

```html
<EditForm Model="DemoJson">
    <JsonEditor @bind-Value="DemoJson.Json"
                FieldName="@nameof(IndexModel.Json)"
                ValidationFor="@(() => DemoJson.Json)"
                AllowEdit="false">
    </JsonEditor>
</EditForm>
```

Blazor.JsonEditor doesn't work without EditForm. Also, validation is required.

# Customization

## Editor template:

You can customize and pass your own editor template as a dynamic component. The editor template is everything that you see after the property name and value like buttons and the editor window itself. For this, you need to set the parameter CustomEditor. As an example **CustomEditor="typeof([JsonItemCustomEditor](https://github.com/joghyrt/Blazor.JsonEditor/tree/main/Blazor.JsonEditor.Demo/Component))"**

```html
<EditForm Model="DemoJson">
    <JsonEditor @bind-Value="DemoJson.Json"
                FieldName="@nameof(IndexModel.Json)"
                ValidationFor="@(() => DemoJson.Json)"
                CustomEditor="typeof(JsonItemCustomEditor)">
    </JsonEditor>
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

Also on add and edit, you need to pass data to **JsonHelper**. We have two methods for this: AddNodeValue and EditNodeValue

```c#
public static void AddNodeValue(JsonObject jsonObject, JsonItem jsonItem)

public static void EditNodeValue(JsonObject jsonObject, JsonItem jsonItem, string editPropertyName)
```

After Add/Edit/Delete do not forget to Invoke JsonObjectChanged

```c#
await JsonObjectChanged.InvokeAsync(JsonObject);
```

## View templates:

You can customize the view template for an item and an object. Item is key: value template. An object view template is an object template that contains a lot of items template in it.

```json
{
  "Nullable" : null, //item view template
  "String" : "random", //item view template
  "Number" : 1, //item view template
  "Array" : [1,2,3], //item view template
  "True" : true, //item view template
  "False" : false, //item view template
  "Object" : { //!OBJECT! view template
    "Nullable" : null, //item view template
    "String" : "random", //item view template
    "Number" : 1, //item view template
    "Array" : [1,2,3], //item view template
    "True" : true, //item view template
    "False" : false //item view template
  }
}
```

### Item view template:

```html
<EditForm Model="DemoJson">
    <JsonEditor @bind-Value="DemoJson.Json"
                FieldName="@nameof(IndexModel.Json)"
                ValidationFor="@(() => DemoJson.Json)"
                CustomItemView="typeof(JsonItemCustomView)">
    </JsonEditor>
</EditForm>
```

In your custom component you need to have parameters:

```c#
[Parameter]
public KeyValuePair<string, JsonNode?> JsonItem { get; set; }
```

You can find an example in [repository](https://github.com/joghyrt/Blazor.JsonEditor/tree/main/Blazor.JsonEditor.Demo.Wasm/Component).

### Object view template:

```html
<EditForm Model="DemoJson">
    <JsonEditor @bind-Value="DemoJson.Json"
                FieldName="@nameof(IndexModel.Json)"
                ValidationFor="@(() => DemoJson.Json)"
                CustomObjectView="typeof(JsonObjectCustomView)">
    </JsonEditor>
</EditForm>
```

In your custom component you need to have parameters:

```c#
[Parameter]
public KeyValuePair<string, JsonNode?> JsonItem { get; set; }
        
[Parameter]
public EventCallback<string?> ValueChanged { get; set; }

[Parameter]
public Dictionary<string, string>? KeyValues { get; set; }
        
[Parameter] 
public bool AllowEdit { get; set; } = true;
        
[Parameter] 
public Type? CustomEditor { get; set; }
        
[Parameter] 
public Type? CustomItemView { get; set; }
        
[Parameter] 
public Type? CustomObjectView { get; set; }
```

Don't forget to include **InternalJsonEditor** inside you custom template to allow build lower object level.

Example:

```html
<InternalJsonEditor Value="@JsonItem.Value.ToJsonString()"
                    ValueChanged="ValueChanged"
                    KeyValues="KeyValues"
                    AllowEdit="AllowEdit"
                    CustomEditor="CustomEditor"
                    CustomItemView="CustomItemView"
                    CustomObjectView="CustomObjectView">
</InternalJsonEditor>
```

You can find an example in [repository](https://github.com/joghyrt/Blazor.JsonEditor/tree/main/Blazor.JsonEditor.Demo.Wasm/Component).

## Next steps

- Refactor

## If you like it please support:

[Buy me a coffee](https://www.buymeacoffee.com/joghyrt)

<img width="120" alt="bmc_qr" src="https://github.com/joghyrt/Blazor.JsonEditor/assets/26901105/d914e23c-dc90-483b-98df-e977c6662672">


[Donate](https://ko-fi.com/joghyrt) 






