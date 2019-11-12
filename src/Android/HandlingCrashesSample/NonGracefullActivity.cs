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
    [Activity(Label = "Non Gracefull")]
    public class NonGracefullActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Debug(this.GetType().Name, "Being OnCreate");

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.NonGracefullView);

            // get our button references
            Button crasherButton = FindViewById<Button>(Resource.Id.ShowCrasherButton);
            crasherButton.Click += (s, e) =>
            {
                StartActivity(typeof(CrashActivity));
            };

            Log.Debug(this.GetType().Name, "End OnCreate");
        }
    }
}