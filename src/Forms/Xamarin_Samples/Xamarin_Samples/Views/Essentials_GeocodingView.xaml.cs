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
    public partial class Essentials_GeocodingView : ContentPage
    {
        public Essentials_GeocodingView()
        {
            InitializeComponent();

            GetLocationForAddress();
            GetPlacemarkForAddress();
        }

        public async Task GetLocationForAddress()
        {
            try
            {
                var address = "Microsoft Building 25 Redmond WA USA";
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    var items = new[]
                    {
                        $"Accuracy:             {location.Accuracy}",
                        $"Altitude:             {location.Altitude}",
                        $"Latitude:             {location.Latitude}",
                        $"Longitude:            {location.Longitude}",
                        $"Course:               {location.Course}",
                        $"IsFromMockProvider:   {location.IsFromMockProvider}",
                        $"Speed:                {location.Speed}",
                        $"Timestamp:            {location.Timestamp}",
                    };

                    lstLocationInfo.ItemsSource = items;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                lstLocationInfo.ItemsSource = new[] { fnsEx.Message };
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
                lstLocationInfo.ItemsSource = new[] { ex.Message };
            }
        }

        public async Task GetPlacemarkForAddress()
        {
            try
            {
                var lat = 47.673988;
                var lon = -122.121513;

                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    var items = new[]
                    {
                        $"AdminArea:        {placemark.AdminArea}",
                        $"CountryCode:      {placemark.CountryCode}",
                        $"CountryName:      {placemark.CountryName}",
                        $"FeatureName:      {placemark.FeatureName}",
                        $"Locality:         {placemark.Locality}",
                        $"Location:         {placemark.Location}",
                        $"PostalCode:       {placemark.PostalCode}",
                        $"SubAdminArea:     {placemark.SubAdminArea}",
                        $"SubLocality:      {placemark.SubLocality}",
                        $"SubThoroughfare:  {placemark.SubThoroughfare}",
                        $"Thoroughfare:     {placemark.Thoroughfare}",
                    };

                    lstPlacemarkInfo.ItemsSource = items;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                lstPlacemarkInfo.ItemsSource = new[] { fnsEx.Message };
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
                lstPlacemarkInfo.ItemsSource = new[] { ex.Message };
            }
        }
    }
}