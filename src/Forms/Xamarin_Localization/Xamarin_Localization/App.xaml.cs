using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Localization.Globalization;
using Xamarin_Localization.Resources;

namespace Xamarin_Localization
{
    public partial class App : Application
    {
        public static string CurrentLanguage = "en-US";
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            ListAllEmbeddedResourceNamesDevOnly();
            EnsureRightResourceCulture();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// Troubleshooting Embedded Images
        /// </summary>
        private void ListAllEmbeddedResourceNamesDevOnly()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine($"Found resource: {res}");
            }

            var temp = new global::System.Resources.ResourceManager("Xamarin_Localization.Resources.AppResources", typeof(AppResources).Assembly);
        }

        private void EnsureRightResourceCulture()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods


                SetLanguage();
            }
        }

        private void SetLanguage() // TODO:
        {
            MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(App.CurrentLanguage));
        }
    }
}
