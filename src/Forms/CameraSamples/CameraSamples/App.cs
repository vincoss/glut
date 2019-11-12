using CameraSamples.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;


namespace CameraSamples
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new SampleOneView();
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

        public void OnShouldTakePicture()
        {
            var handler = this.ShouldTakePicture;
            if(handler != null)
            {
                handler();
            }
        }

        public event Action ShouldTakePicture = () => { };

        public Action<string> SetImagePath { get; set; }
    }
}