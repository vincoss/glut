using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Essentials_DeviceDisplayInformationView : ContentPage
    {
        public Essentials_DeviceDisplayInformationView()
        {
            InitializeComponent();

            var info = DeviceDisplay.MainDisplayInfo;

            var items = new[]
            {
                $"BuildString:              {DeviceDisplay.KeepScreenOn}",
                $"MainDisplay-Density:      {info.Density}",
                $"MainDisplay-Orientation:  {info.Orientation}",
                $"MainDisplay-Rotation:     {info.Rotation}",
                $"MainDisplay-Height:       {info.Height}",
                $"MainDisplay-Width:        {info.Width}",
            };

            lstInfo.ItemsSource = items;
        }
    }
}