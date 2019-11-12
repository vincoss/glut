using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Essentials_LauncherView : ContentPage
    {
        public Essentials_LauncherView()
        {
            InitializeComponent();
        }

        private void btnOpen_Clicked(object sender, EventArgs e)
        {
            OpenBrowser();
        }

        private void btnFiles_Clicked(object sender, EventArgs e)
        {
            OpenFile();
        }

        public async Task OpenBrowser()
        {
            var url = "https://www.google.com.au/";
            var supportsUri = await Launcher.CanOpenAsync(url);
            if (supportsUri)
                await Launcher.OpenAsync(url);
        }

        public async Task OpenFile()
        {
            var fn = "File.txt";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            File.WriteAllText(file, "Hello World");

            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(file)
            });
        }
    }
}