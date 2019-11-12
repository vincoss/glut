using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PagesSamples.Views
{
    public partial class NavigationPageSample : ContentPage
    {
        public NavigationPageSample()
        {
            InitializeComponent();

            NavigateButton.Clicked += NavigateButton_Clicked;
        }

        private async void NavigateButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentPageSample());
        }
    }
}
