using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PersistDataSample
{
    [Activity(Label = "PersistDataSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
          
        }

        protected override void OnPause()
        {
            base.OnPause();

            // NOTE: Save persistent data here.
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            // NOTE: This method is not part of the lifecycle callbacks, so will not be called in every situation as described in its documentation.
            // DO NOT: Persist data in here since it may not be called.
        }
    }
}

