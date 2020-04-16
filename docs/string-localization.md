# String Localization
Localization is the process of adapting an application to a specific country or region. To do this, an application should translate an application’s resources such as text, images into multiple cultures. So, an application shows localized resources based on the settings of device.

This post explains how to localize a text in Xamarin.Forms Tizen Application. I strongly recommend you to read [Xamarin.Forms String and Image Localization](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/localization/text) before you get started. You can obtain the sample application [here](https://github.com/edenlee1212/LocalizationSample).

## Create resource files
The .NET framework provides a way for localization an application using [Resx resource files](https://docs.microsoft.com/dotnet/framework/resources/creating-resource-files-for-desktop-apps). Each item consists of a name/value pair and an application can retrieve specific resource by name. By using this mechanism, you can store and retrieve localized texts.

   1. Create a default resource file 
   By using **Add New Item dialog**, add a default resource file, the name of which doesn’t contain any locale information. In the sample application, **Resources.resx** is added in [Strings](https://github.com/edenlee1212/LocalizationSample/tree/master/LocalizationSample/Strings) folder.
   
      ![AddNewItem](https://user-images.githubusercontent.com/1029182/79188146-37b4b400-7e59-11ea-924d-e6e7653127ba.png)      
   Set the **Access Modifier** to **Public**, which results in a file with the **.designer.cs** extension being added to the project. Then add items which contain the following information:
      * **Name** : The key used to retrieve the text
      * **Value** : The localized text
      * **Comment** : Addition information (optional)
      
      ![Resources](https://user-images.githubusercontent.com/1029182/79188384-d3462480-7e59-11ea-89fb-8fdd7e47fe31.png)

   2. Add additional resource files   
   Create additional resource files for each locale you want to support. The name of each resource file should include the locale information such as **Resources.ko-KR.resx**. In these resources files, set the **Access Modifier** to **No code gen**.
   
      ![Resources ko](https://user-images.githubusercontent.com/1029182/79189053-b6125580-7e5b-11ea-96b9-9b022738ec78.png)     
   The name of resource file can contain only language without any country code such as **Resource.es.resx**. If culture is **es-ES**, the application looks for resource files in this order:
      1. Resources.es-ES.resx
      2. Resources.es.resx
      3. Resources.resx (default)
      
## Specify the default culture
Specify the default culture by **NeutralResourcesLanguage** to inform the resource manager of app’s default culture. This improves lookup performance for the first resource to be loaded. In the sample application, **en** is set as **NeutralResourcesLanguage** in [Main.cs](https://github.com/edenlee1212/LocalizationSample/blob/master/LocalizationSample/Main.cs). For more information about NeutralResourcesLanguage, see [NeutralResourcesLanguage Attribute Class](https://docs.microsoft.com/dotnet/api/system.resources.neutralresourceslanguageattribute).
```
using System.Resources;

[assembly: NeutralResourcesLanguage("en")]
```

## Localize texts
The language setting of device can be changed when your application is running. To adapt your application to the changed culture, texts need to be translated on the fly. [XAML markup extensions](https://docs.microsoft.com/xamarin/xamarin-forms/xaml/markup-extensions/) can be a good solution to meet this requirement and this is where we will start with my own **LocalizedResourceExtension** class.
```
[ContentProperty("Name")]
public class LocalizedResourceExtension : IMarkupExtension<BindingBase>
{
    private static readonly IValueConverter _converter = new LocalizedResourceConverter();

    public string Name { get; set; }

    public BindingBase ProvideValue(IServiceProvider serviceProvider)
    {
        return new Binding(nameof(LocalizationService.UICulture), BindingMode.OneWay, converter: _converter, converterParameter: Name, source: LocalizationService.Instance);
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
    }
}
```
This extension enables a text to be localized when an application is started and UI culture is changed. This extension has a single property named **Name** of type string that you set to the name of text to be localized. This **Name** property is the content property, so **Name=** is not required.

I also made **LocalizationService** class, which provides information about current UI culture and notifies clients that current UI culture has changed. You can see the code [here](https://github.com/edenlee1212/LocalizationSample/blob/master/LocalizationSample/Services/LocalizationService.cs).

In addition, **LocalizationService** class provides a method to get localized text. This function uses **ResourceManager** defined in **Resources.Designer.cs**.
```
/// <summary>
/// Get the value of localized resource.
/// </summary>
/// <param name="name">Resource name</param>
/// <returns>
/// The value of localized resource. If name is not found in all resource files, then name is returned.
/// </returns>
public string GetResource(string name)
{
    return Resources.ResourceManager.GetString(name, UICulture) ?? name;
}
```
By using this method, **LocalizedResourceConverter** class converts a text to the localized text.
```
public class LocalizedResourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return LocalizationService.Instance.GetResource(parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

Accessing the value of text by **LocalizedResourceExtension** enables a localized text to be displayed In XAML even if the language setting of device has been changed.
```
<c:CirclePage
    x:Class="LocalizationSample.Views.CenterLayoutPage"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:c="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
    xmlns:ext="clr-namespace:LocalizationSample.Extensions">

    <c:CirclePage.Content>
        <StackLayout Margin="0, 40, 0, 30" VerticalOptions="CenterAndExpand">
            <Label
                Text="{ext:LocalizedResource CountryName}"
                HorizontalOptions="CenterAndExpand" />
        </StackLayout>
    </c:CirclePage.Content>

</c:CirclePage>
```

## Sample application demo
You can see that the text on the page is changed to the localized text when current UI culture has been changed.


