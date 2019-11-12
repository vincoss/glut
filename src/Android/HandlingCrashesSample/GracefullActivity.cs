using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace HandlingCrashesSample
{
    [Activity(Label = "Gracefull")]
    public class GracefullActivity : ActivityBase
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Debug(this.GetType().Name, "Being OnCreate");

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.GracefullView);
            UpdateAppInitStatus();

            // get our button references
            Button crasherButton = FindViewById<Button>(Resource.Id.ShowCrasherButton);
            crasherButton.Click += (s, e) =>
                {
                    StartActivity(typeof (CrashActivity));
                };

            Log.Debug(this.GetType().Name, "End OnCreate");
        }

        protected void UpdateAppInitStatus()
        {
            var textView = FindViewById<TextView>(Resource.Id.DetailTextView);
            textView.Text = "App Initialized: " + App.Current.IsInitialized;
        }

        protected override void FinishedInitializing()
        {
            Log.Debug("!!!!!! IntermediateScreen", "IntermediateScreen.FinishedInitializing");
            this.UpdateAppInitStatus();
        }
    }
}