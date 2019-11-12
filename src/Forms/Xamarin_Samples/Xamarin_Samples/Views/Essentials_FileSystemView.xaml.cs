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
    public partial class Essentials_FileSystemView : ContentPage
    {
        public Essentials_FileSystemView()
        {
            InitializeComponent();

            var items = new[]
           {
                $"CacheDirectory:   {FileSystem.CacheDirectory}",
                $"AppDataDirectory: {FileSystem.AppDataDirectory}"
            };

            lstInfo.ItemsSource = items;
        }
    }
}