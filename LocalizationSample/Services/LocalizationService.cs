using System;
using System.ComponentModel;
using System.Globalization;
using Tizen.Applications;
using Tizen.System;

using LocalizationSample.Strings;

namespace LocalizationSample.Services
{
    /// <summary>
    /// Provides functionalities for obtaining the localized resources.
    /// </summary>
    public partial class LocalizationService : INotifyPropertyChanged
    {
        private static readonly Lazy<LocalizationService> _lazyInstance = new Lazy<LocalizationService>(() => new LocalizationService());

        private LocalizationService()
        {
            UpdateUICulture();
            UpdateCulture();
            (Application.Current as CoreApplication).LocaleChanged += OnLocaleChanged;
            (Application.Current as CoreApplication).RegionFormatChanged += OnRegionFormatChanged;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the UI culture changes.
        /// </summary>
        public event EventHandler<CultureChangedEventArgs> UICultureChanged;

        /// <summary>
        /// Occurs when the culture changes.
        /// </summary>
        public event EventHandler<CultureChangedEventArgs> CultureChanged;

        /// <summary>
        /// Gets an instance of LocalizationService.
        /// </summary>
        public static LocalizationService Instance => _lazyInstance.Value;

        /// <summary>
        /// Gets the current culture.
        /// This culture is used when handling dates, times, and numeric values.
        /// For more details, see https://docs.microsoft.com/dotnet/standard/globalization-localization/globalization.
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Gets the current UI culture.
        /// This culture is used when handling resources.
        /// For more details, see https://docs.microsoft.com/dotnet/standard/globalization-localization/globalization.
        /// </summary>
        public CultureInfo UICulture { get; private set; }

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

        private void UpdateUICulture()
        {          
            UICulture = new CultureInfo(SystemSettings.LocaleLanguage.Replace('_', '-'));
            Logger.Info($"CurrentUICulture: {UICulture?.ToString()}");

            OnPropertyChanged(nameof(UICulture));
            UICultureChanged?.Invoke(this, new CultureChangedEventArgs(UICulture));
        }

        private void UpdateCulture()
        {
            Culture = CultureInfo.CurrentCulture;
            Logger.Info($"CurrentCulture: {Culture?.ToString()}");
            
            OnPropertyChanged(nameof(Culture));
            CultureChanged?.Invoke(this, new CultureChangedEventArgs(Culture));
        }

        private void OnLocaleChanged(object sender, LocaleChangedEventArgs e)
        {
            Logger.Info($"OnLocaleChanged: {e.Locale}");
            UpdateUICulture();
        }

        private void OnRegionFormatChanged(object sender, RegionFormatChangedEventArgs e)
        {
            Logger.Info($"OnRegionFormatChanged: {e.Region}");
            UpdateCulture();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Event arguments for CultureChanged
    /// </summary>
    public class CultureChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CultureChangedEventArgs.
        /// </summary>
        /// <param name="culture">The current culture</param>
        public CultureChangedEventArgs(CultureInfo culture)
        {
            Culture = culture;
        }

        /// <summary>
        /// Gets the current culture.
        /// </summary>
        public CultureInfo Culture { get; private set; }
    }
}
