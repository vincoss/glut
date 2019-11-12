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
    public partial class EssentialsVersionTrackingView : ContentPage
    {
        public EssentialsVersionTrackingView()
        {
            InitializeComponent();

            var items = new[]
           {
                $"BuildHistory:   {VersionTracking.BuildHistory}",
                $"CurrentBuild:   {VersionTracking.CurrentBuild}",
                $"CurrentVersion:   {VersionTracking.CurrentVersion}",
                $"FirstInstalledBuild:   {VersionTracking.FirstInstalledBuild}",
                $"FirstInstalledVersion:   {VersionTracking.FirstInstalledVersion}",
                $"IsFirstLaunchEver:   {VersionTracking.IsFirstLaunchEver}",
                $"IsFirstLaunchForCurrentBuild:   {VersionTracking.IsFirstLaunchForCurrentBuild}",
                $"IsFirstLaunchForCurrentVersion:   {VersionTracking.IsFirstLaunchForCurrentVersion}",
                $"PreviousBuild:   {VersionTracking.PreviousBuild}",
                $"PreviousVersion:   {VersionTracking.PreviousVersion}",
                $"VersionHistory:   {VersionTracking.VersionHistory}",
            };

            lstInfo.ItemsSource = items;
        }
    }
}