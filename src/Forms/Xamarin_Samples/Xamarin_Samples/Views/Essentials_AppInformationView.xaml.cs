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
    public partial class Essentials_AppInformationView : ContentPage
    {
        public Essentials_AppInformationView()
        {
            InitializeComponent();

            var items = new[]
            {
                $"BuildString:   {AppInfo.Name}",
                $"BuildString:   {AppInfo.BuildString}",
                $"PackageName:   {AppInfo.PackageName}",
                $"Version:       {AppInfo.Version}",
                $"VersionString: {AppInfo.VersionString}"
            };

            lstInfo.ItemsSource = items;
        }

        private void btnShow_Clicked(object sender, EventArgs e)
        {
            AppInfo.ShowSettingsUI();
        }
    }
}