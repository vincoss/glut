using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace SavingActivityStateSample
{
    [Activity(Label = "Saving Activity State", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int _counter = 0;

        protected override void OnCreate(Bundle bundle)
        {
            Log.Debug(GetType().FullName, "Activity A - OnCreate");

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var openActivityButton = FindViewById<Button>(Resource.Id.MyButton);

            openActivityButton.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(SecondActivity));
                intent.PutExtra("click_count", _counter);
                StartActivity(intent);
            };

            if (bundle != null)
            {
                _counter = bundle.GetInt("click_count", 0);
                Log.Debug(GetType().FullName, "Recovered instance state");
            }

            var clickbutton = FindViewById<Button>(Resource.Id.clickButton);
            clickbutton.Text = Resources.GetString(Resource.String.CounterButton, _counter);
            clickbutton.Click += (object sender, System.EventArgs e) =>
            {
                _counter++;
                clickbutton.Text = Resources.GetString(Resource.String.CounterButton, _counter);
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

            var textView = FindViewById<TextView>(Resource.Id.TextDetail);
            textView.Text = string.Format("Count : {0}", _counter);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("click_count", _counter);

            Log.Debug(GetType().FullName, "Saving instance state");

            base.OnSaveInstanceState(outState);
        }
    }
}

