using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace HandlingCrashesSample
{
    [Activity(Label = "Handling Crashes", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ActivityBase
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Debug(this.GetType().Name, "Being OnCreate");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var gracefullButton = FindViewById<Button>(Resource.Id.GracefullButton);
            gracefullButton.Click += (s, e) =>
                {
                    StartActivity(typeof(GracefullActivity));
                };

            var nonGracefullButton = FindViewById<Button>(Resource.Id.NonGracefullButton);
            nonGracefullButton.Click += (s, e) =>
            {
                StartActivity(typeof(NonGracefullActivity));
            };

            Log.Debug(this.GetType().Name, "Edn OnCreate");
        }

        protected override void FinishedInitializing()
        {
            Log.Debug("!!!!!!! MainActivity", "MainActivity.FinishedInitializing Called");
        }
    }
}

