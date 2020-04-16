using System;
using System.Windows.Input;

using LocalizationSample.Mvvm;

using Xamarin.Forms;

namespace LocalizationSample.ViewModels
{
    public class CenterLayoutViewModel : BaseViewModel
    {
        public ICommand ClickButtonCommand { get; private set; }

        public CenterLayoutViewModel()
        {
            ClickButtonCommand = new Command(() => ClickButton());
        }

        // Called when the button changed by click.
        private void ClickButton()
        {
            // TODO TTS: Insert code to handle the button clicked.
            //
            // To perform page navigation, use the GoToAsync method in Xamarin.Forms Shell API.
            // await Shell.Current.GoToAsync("newpage");
            //
            // For more details, see https://docs.microsoft.com/ko-kr/xamarin/xamarin-forms/app-fundamentals/shell/navigation
        }
    }
}
