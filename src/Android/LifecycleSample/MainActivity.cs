using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace LifecycleSample
{
    [Activity(Label = "Lifecycle Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        // Activity Lifecycle methods. Order as they are excecuted.
        // NOTE: To test this scenario use Home button and Back button

        protected override void OnCreate(Bundle bundle)
        {
            Log.Debug(this.GetType().Name, "Begin OnCreate");

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var textView = FindViewById<TextView>(Resource.Id.TextDetail);

            Log.Debug(this.GetType().Name, "End   OnCreate");
        }

        protected override void OnRestart()
        {
            Log.Debug(this.GetType().Name, "Begin OnRestart");

            base.OnRestart();

            Log.Debug(this.GetType().Name, "Begin OnRestart");
        }

        protected override void OnStart()
        {
            Log.Debug(this.GetType().Name, "Begin OnStart");

            base.OnStart();

            Log.Debug(this.GetType().Name, "End   OnStart");
        }

        protected override void OnResume()
        {
            Log.Debug(this.GetType().Name, "Begin OnResume");

            base.OnResume();

            Log.Debug(this.GetType().Name, "End   OnResume");
        }

        protected override void OnPause()
        {
            Log.Debug(this.GetType().Name, "Begin OnPause");

            base.OnPause();

            Log.Debug(this.GetType().Name, "End   OnPause");
        }

        protected override void OnStop()
        {
            Log.Debug(this.GetType().Name, "Begin OnStop");

            base.OnStop();

            Log.Debug(this.GetType().Name, "End   OnStop");
        }

        protected override void OnDestroy()
        {
            Log.Debug(this.GetType().Name, "Begin OnDestroy");

            base.OnDestroy();

            Log.Debug(this.GetType().Name, "Begin OnDestroy");
        }
    }
}

