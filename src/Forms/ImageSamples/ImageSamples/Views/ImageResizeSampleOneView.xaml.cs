using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;


namespace ImageSamples.Views
{
    // NOTE: Might throw outofmemory exception

    public partial class ImageResizeSampleOneView : ContentPage
    {
        public static string ResourcePrefix;

        public ImageResizeSampleOneView()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
            {
                ResourcePrefix = "ImageSamples.iOS.";
            }
            else if (Device.OS == TargetPlatform.Android)
            {
                ResourcePrefix = "ImageSamples.";
            }
            else if (Device.OS == TargetPlatform.WinPhone)
            {
                ResourcePrefix = "ImageSamples.WinPhone.";
            }

            ResizeImageButton.Clicked += ResizeImageButton_Clicked;

           // this.PhotoResizeImage.Source = ImageSource.FromStream(() => GetImageStream(ResourcePrefix + "OriginalImage.JPG"));
        }

        private void ResizeImageButton_Clicked(object sender, EventArgs e)
        {
            this.ResizeImage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var assembly = typeof(ImageResizeSampleOneView).GetTypeInfo().Assembly;

            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine("found resource: {0}", res);
            }
        }

        protected void ResizeImage()
        {
            var assembly = typeof(ImageResizeSampleOneView).GetTypeInfo().Assembly;
            byte[] imageData;

            Stream stream = assembly.GetManifestResourceStream(ResourcePrefix + "OriginalImage.JPG");
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imageData = ms.ToArray();
            }

            byte[] resizedImage = ImageResizer.ResizeImage(imageData, 64, 64);

            this.PhotoResizeImage.Source = ImageSource.FromStream(() => new MemoryStream(resizedImage));
        }

        //private Stream GetImageStream(string path)
        //{
        //    var assembly = typeof(ImageResizeSampleOneView).GetTypeInfo().Assembly;
        //    var stream = assembly.GetManifestResourceStream(path);
        //    return stream;
        //}
    }
}
