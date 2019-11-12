using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Navigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingTwoView : ContentPage
    {
        public SettingTwoView()
        {
            InitializeComponent();
        }

        private async void btnClose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            //NavigationPage.SetHasNavigationBar(this, true); //get your navbar back
            //NavigationPage.SetHasBackButton(this, true); //get your back button back

            base.OnAppearing();
        }
    }
}