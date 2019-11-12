using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SavingActivityStateSample
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Second);

            var homeButton = FindViewById<Button>(Resource.Id.HomeButton);
            homeButton.Click += (s, e) =>
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                };

            var textView = FindViewById<TextView>(Resource.Id.TextDetail);

            var counter = Intent.Extras.GetString("click_count");
            textView.Text = string.Format("Count : {0}", counter);
        }
    }
}