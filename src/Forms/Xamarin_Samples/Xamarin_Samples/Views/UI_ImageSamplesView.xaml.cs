using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_ImageSamplesView : ContentPage
    {
        public UI_ImageSamplesView()
        {
            InitializeComponent();

            var image = new Image { Source = "temp.png" };
            // Or show path for each platform
            //image.Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("temp.png") : ImageSource.FromFile("resources/temp.png");
            images.Children.Add(image);

            var eimg = ImageSource.FromResource("Xamarin_Samples.Resources.embeddedtemp.png", typeof(UI_ImageSamplesView).GetTypeInfo().Assembly);
            var embeddedImage = new Image { Source = eimg };
            images.Children.Add(embeddedImage);

            // Downloading images
            var webImage = new Image { Source = ImageSource.FromUri(new Uri("https://xamarin.com/content/images/pages/forms/example-app.png")) };

            // No caching
            image.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri("http://server.com/image") };

            // Caching
            webImage.Source = new UriImageSource
            {
                Uri = new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(5, 0, 0, 0)
            };
        }
    }
}