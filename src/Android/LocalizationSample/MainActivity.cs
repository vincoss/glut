using System;
using System.Globalization;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;

namespace LocalizationSample
{
    [Activity(Label = "Localization Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);


            
            var button = FindViewById<Button>(Resource.Id.ButtonLanguage);

            button.Click += button_Click;
        }

        private void button_Click(object sender, EventArgs e)
        {
            // Set locale
            Resources resources = Resources;
            Configuration configuration = resources.Configuration;

            if (configuration.Locale.Language == "au")
            {
                configuration.Locale = new Java.Util.Locale("kr");
            }
            else if (configuration.Locale.Language == "kr")
            {
                configuration.Locale = new Java.Util.Locale("sk");
            }
            else
            {
                configuration.Locale = new Java.Util.Locale("au");
            }

            DisplayMetrics displayMetrics = resources.DisplayMetrics;
            resources.UpdateConfiguration(configuration, displayMetrics);

            RestartApplication();
        }

        private void RestartApplication()
        {
            var intent = new Intent(this, typeof (MagicAppRestart));

            intent.SetFlags(ActivityFlags.ClearTop);
            intent.SetFlags(ActivityFlags.NewTask);

            StartActivity(intent);
        }
    }
}

