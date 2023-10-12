# Blazor.JsonEditor
Json editor for Blazor Server App. Rewrited and working version of **[Blazoring.JsonEditor](https://github.com/vmakharashvili/Blazoring-JsonEditor)** 

## Json Editor tool

* To install the package run following command:

**`Install-Package Blazor.JsonEditor`**
or search **Blazor.JsonEditor** in Nuget gallery.

This will install Blazor.JsonEditor in your project. You also need to add in **_Imports.razor**:
```html
@using Blazor.JsonEditor.Component
```
Also, you need to add javascript file in _Host.cshtml file:

```html
<script src="_content/Blazor.JsonEditor/Blazor.JsonEditor.js"></script>
```
For icons suppor JsonEditor uses Font-Awesome icons library. You need to add link to _Host.cshtml file:
```html
<link rel="stylesheet" href="https://use.fontawesome.com/releases/v6.4.2/css/all.css">
```

### Using in code:

```html
<EditForm Model="DemoJson">
    <JsonEditor @bind-Value="DemoJson.Json" FieldName="@nameof(IndexModel.Json)" ValidationFor="@(() => DemoJson.Json)"></JsonEditor>
</EditForm>
```

Blazor.JsonEditor doesn't work without EditForm. Also, validation is required.