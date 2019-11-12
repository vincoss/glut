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
    public partial class Essentials_DeviceInfoView : ContentPage
    {
        public Essentials_DeviceInfoView()
        {
            InitializeComponent();

            var items = new[]
            {
                $"Model:        {DeviceInfo.Model}",
                $"DeviceType:   {DeviceInfo.DeviceType}",
                $"Idiom:        {DeviceInfo.Idiom}",
                $"Manufacturer: {DeviceInfo.Manufacturer}",
                $"Name:         {DeviceInfo.Name}",
                $"Platform:     {DeviceInfo.Platform}",
                $"Version:      {DeviceInfo.Version}",
                $"VersionString:{DeviceInfo.VersionString}",
            };

            lstInfo.ItemsSource = items;
        }
    }
}