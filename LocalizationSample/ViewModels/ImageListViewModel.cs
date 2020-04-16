using System;
using System.Collections.Generic;
using System.Windows.Input;

using LocalizationSample.Models;
using LocalizationSample.Mvvm;
using LocalizationSample.Services;

using Xamarin.Forms;

namespace LocalizationSample.ViewModels
{
    public class ImageListViewModel : BaseViewModel
    {
        public IEnumerable<SampleColor> AllColors => SampleDataService.AllColors();

        public ICommand SelectedItemCommand { get; private set; }
        public ICommand TappedItemCommand { get; private set; }

        public ImageListViewModel()
        {
            SelectedItemCommand = new Command<SampleColor>((o) => SelectedItem(o));
            TappedItemCommand = new Command<SampleColor>((o) => TappedItem(o));
        }

        // Called once when the item is selected.
        private void SelectedItem(object value)
        {
            // TODO TTS : Insert code to handle the list item selected command.
            // var selectedValue = value as SampleColor;
            // if (selectedValue != null)
            // {
            //     Logger.Info($"Selected Item : {selectedValue.Name}");
            // }
        }

        // Called when the item is tapped every time.
        private void TappedItem(object value)
        {
            // TODO TTS : Insert code to handle the list item tapped command.
            // var tappedValue = value as SampleColor;
            // if (tappedValue != null)
            // {
            //     Logger.Info($"Tapped Item : {tappedValue.Name}");
            // }
        }
    }
}
