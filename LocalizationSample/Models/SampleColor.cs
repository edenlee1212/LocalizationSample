using System.ComponentModel;

using LocalizationSample.Services;
using LocalizationSample.Extensions;

namespace LocalizationSample.Models
{
    // This is used by the SampleDataService.
    public class SampleColor : INotifyPropertyChanged
    {
        public SampleColor()
        {
            LocalizationService.Instance.UICultureChanged += OnUICultureChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }

        public string DisplayName => Name.Localize();

        public string HexCode { get; set; }

        public string Icon { get; set; }

        public override string ToString()
        {
            return $"{DisplayName} {HexCode}";
        }

        private void OnUICultureChanged(object sender, CultureChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
        }
    }
}
