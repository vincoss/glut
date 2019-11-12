using ImageSamples.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ImageSamples
{
    public class App : Application
    {
        public App()
        {
            MainPage = new ImageResizeSampleOneView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Func<string, int, int, Stream> GetImage;
    }
}
