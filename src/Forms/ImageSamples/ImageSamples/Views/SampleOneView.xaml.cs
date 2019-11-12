using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ImageSamples.Views
{
    public partial class SampleOneView : ContentPage
    {
        public SampleOneView()
        {
            InitializeComponent();

            Sample();
        }

        private void Sample()
        {
            // NOTE: To test this create large images in the Photo/CorePhoto directory. 
            // Can display just one image but if more images then OutOfMemory exception is thrown.

            // Sample One - Throws if two or more images are displayed.
            //var a = UriImageSource.FromFile("/storage/emulated/0/Pictures/CorePhoto/A.jpg");
            //var b = UriImageSource.FromFile("/storage/emulated/0/Pictures/CorePhoto/B.jpg");

            //OneImage.Source = a;
            //TwoImage.Source = b;

            // Sample Two - Works but image must be resized.
            //var a = App.GetImage("/storage/emulated/0/Pictures/CorePhoto/Temp.jpg", 100, 100);
            //var b = App.GetImage("/storage/emulated/0/Pictures/CorePhoto/Temp.jpg", 100, 100);

            //OneImage.Source = ImageSource.FromStream(() =>
            //    {
            //        return a;
            //    });

            //TwoImage.Source = ImageSource.FromStream(() =>
            //{
            //    return b;
            //});

            // Sample Three - Does not works. Throws Java.Lang.OutOfMemoryError: 

            //for (int i = 0; i < LayoutRoot.Children.Count; i++)
            //{
            //    var image = (Image)LayoutRoot.Children[i];
            //    image.Source = ImageSource.FromStream(() => { return App.GetImage("/storage/emulated/0/Pictures/CorePhoto/Temp.jpg", 100, 100); });

            //    LayoutRoot.Children.Add(image);
            //}

            // Sample Four - Works
            for (int i = 0; i < LayoutRoot.Children.Count; i++)
            {
                var stream = App.GetImage("/storage/emulated/0/Pictures/CorePhoto/Temp.jpg", 100, 100);

                var image = (Image)LayoutRoot.Children[i];
                image.Source = ImageSource.FromStream(() => { return stream; });
            }
        }
    }
}
