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
    public partial class EssentialsShareView : ContentPage
    {
        public EssentialsShareView()
        {
            InitializeComponent();
        }

        private async void btnShareText_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "Hi there!!!",
                Title = "Share Text"
            });
        }

        private async void btnShareUrl_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = "https://docs.microsoft.com/",
                Title = "Share Web Link"
            });
        }

        private async void btnShareFile_Clicked(object sender, EventArgs e)
        {
            var fn = "Attachment.txt";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            File.WriteAllText(file, "Hello World");

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(file)
            });
        }
    }
}