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
    public partial class Essentials_MapView : ContentPage
    {
        public Essentials_MapView()
        {
            InitializeComponent();
        }

        private void btnLocation_Clicked(object sender, EventArgs e)
        {
            OnLocation();
        }

        private void tbnPlacemark_Clicked(object sender, EventArgs e)
        {
            OnPlacemark();
        }

        private void btnDirection_Clicked(object sender, EventArgs e)
        {
            OnDirection();
        }

        public async Task OnLocation()
        {
            var location = new Location(47.645160, -122.1306032);
            var options = new MapLaunchOptions { Name = "Microsoft Building 25" };

            await Map.OpenAsync(location, options);
        }

        public async Task OnPlacemark()
        {
            var placemark = new Placemark
            {
                CountryName = "United States",
                AdminArea = "WA",
                Thoroughfare = "Microsoft Building 25",
                Locality = "Redmond"
            };
            var options = new MapLaunchOptions { Name = "Microsoft Building 25" };

            await Map.OpenAsync(placemark, options);
        }

        public async Task OnDirection()
        {
            var location = new Location(47.645160, -122.1306032);
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

            await Map.OpenAsync(location, options);
        }
    }
}