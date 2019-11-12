using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Navigation.Models;

namespace Xamarin_Navigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTwoView : ContentPage
    {
        public PageTwoView()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            // Passing Data through a BindingContext
            var contact = new Contact
            {
                Name = "Ferdinand Lukasak",
            };

            var nextPage = new PageThreeView();
            nextPage.BindingContext = contact;
            await Navigation.PushAsync(nextPage);
        }
    }
}