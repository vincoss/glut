using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace DialogSample
{
    [Activity(Label = "DialogSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private AlertDialog _alert;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += (s, e) =>
                {
                    this.ShowAlertDialog();
                };
        }

        /// <summary>
        /// pause can happen if something is brought in front of the activity, partially obscuring
        /// it and preventing user interaction. need to remove any external event handlers, dismiss
        /// dialogs, pause UI updates, etc.
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();

            // if we don't dismiss this, and it's shown, it'll get leaked because it's 
            // actually tied to a window context, rather than the Activity.
            if (_alert != null)
            {
                _alert.Dismiss();
            }
        }

        /// <summary>
        /// just a helper method to build and show our alert dialog
        /// </summary>
        protected void ShowAlertDialog()
        {
            _alert = new AlertDialog.Builder(this).Create();
            _alert.SetMessage("An AlertDialog! Don't forget to clean me up!");
            _alert.SetTitle("Hey Lala!");
            _alert.SetButton("Ohkaay!", (s, e) =>
            {
                _alert.Dismiss();
            });
            _alert.Show();
        }
    }
}

