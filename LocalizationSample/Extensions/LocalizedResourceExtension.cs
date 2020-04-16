using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LocalizationSample.Converters;
using LocalizationSample.Services;

namespace LocalizationSample.Extensions
{
    /// <summary>
    /// A XAML markup extension that provides the binding to the localized string resource.
    /// A resource must be stored using a naming convention for resource files in Strings directory.
    /// If a resource is not found in all resource files, then typed Name is returned instead of null.
    /// Each resource file should include the translation culture in the filename. For example, Resources.es.resx.
    /// For more details, see https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/localization/text?pivots=windows#create-resx-files.
    /// </summary>
    [ContentProperty("Name")]
    public class LocalizedResourceExtension : IMarkupExtension<BindingBase>
    {
        private static readonly IValueConverter _converter = new LocalizedResourceConverter();

        /// <summary>
        /// The name of resource to be localized.
        /// </summary>
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
}
