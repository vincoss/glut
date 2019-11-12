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
    public partial class Essentials_PreferencesView : ContentPage
    {
        public Essentials_PreferencesView()
        {
            InitializeComponent();
        }

        private void btnSet_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("my_key", "my_value");
            var myValue = Preferences.Get("my_key", "default_value");
            Preferences.Remove("my_key");
            Preferences.Clear();

            lblInfo.Text = myValue;
        }
    }
}