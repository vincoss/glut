using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Sqlite.Services;
using Xamarin_Sqlite.Views;

namespace Xamarin_Sqlite
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<SqliteDataStore>();
            MainPage = new MainPage();
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
    }
}
