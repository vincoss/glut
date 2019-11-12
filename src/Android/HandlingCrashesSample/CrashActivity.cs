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
    [Activity(Label = "Crasher")]
    public class CrashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Log.Debug(this.GetType().Name, "Being OnCreate");

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Crash);

            var killProcessButton = FindViewById<Button>(Resource.Id.KillProcessButton);
            killProcessButton.Click += (s, e) =>
                {
                    Process.KillProcess(Process.MyPid());
                };

            var throwExceptionButton = FindViewById<Button>(Resource.Id.ThrowExceptionButton);
            throwExceptionButton.Click += (s, e) =>
                {
                    throw new Exception("Hola exception...");
                };

            Log.Debug(this.GetType().Name, "End OnCreate");
        }
    }
}