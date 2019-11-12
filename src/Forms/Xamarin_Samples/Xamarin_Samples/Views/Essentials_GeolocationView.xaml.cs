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
    public partial class Essentials_GeolocationView : ContentPage
    {
        public Essentials_GeolocationView()
        {
            InitializeComponent();

            GetLastLocation();
            GetLocation();
        }

        public async Task GetLastLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            DisplayLocationInfo(location, true);
        }

        public async Task GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            DisplayLocationInfo(location);
        }

        public void DisplayLocationInfo(Location location, bool isLastLocation = false)
        {
            if (location == null)
            {
                return;
            }

            try
            {
                var items = new[]
                     {
                        $"IsLastLocation:       {isLastLocation}",
                        $"Accuracy:             {location.Accuracy}",
                        $"Altitude:             {location.Altitude}",
                        $"Course:               {location.Course}",
                        $"IsFromMockProvider:   {location.IsFromMockProvider}",
                        $"Latitude:             {location.Latitude}",
                        $"Longitude:            {location.Longitude}",
                        $"Speed:                {location.Speed}",
                        $"Timestamp:            {location.Timestamp}",
                    };

                lstLocation.ItemsSource = items;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                lstLocation.ItemsSource = new[] { fnsEx.Message };
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                lstLocation.ItemsSource = new[] { fneEx.Message };
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                lstLocation.ItemsSource = new[] { pEx.Message };
            }
            catch (Exception ex)
            {
                // Unable to get 
                lstLocation.ItemsSource = new[] { ex.Message };
            }
        }

    }
}