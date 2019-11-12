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
    public partial class Essentials_BrowserView : ContentPage
    {
        public Essentials_BrowserView()
        {
            InitializeComponent();
        }

        private async void btnOpen_Clicked(object sender, EventArgs e)
        {
             await Browser.OpenAsync("http://www.google.com.au/", BrowserLaunchMode.SystemPreferred);
        }

        private async void btnOpenCustom_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync("http://www.google.com.au/", new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.AliceBlue,
                PreferredControlColor = Color.Violet
            });
        }
    }
}