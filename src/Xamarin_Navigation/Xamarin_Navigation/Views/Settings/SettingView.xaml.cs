using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Navigation.Services;
using Xamarin_Navigation.ViewModels;

namespace Xamarin_Navigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingView : ContentPage
    {
        public SettingView()
        {
            InitializeComponent();

           BindingContext = new SettingViewModel(new Navigator());
        }

        protected override void OnAppearing()
        {
            var na = this.Navigation;
            if(na != null)
            {

            }

            base.OnAppearing();
        }
    }
}