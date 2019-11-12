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

namespace AnimationSample
{
    [Activity(Label = "DrawingActivity")]
    public class DrawingActivity : Activity
    {
        private KarmaMeter _karmaMeter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Drawing);

            _karmaMeter = FindViewById<KarmaMeter>(Resource.Id.KarmaMeter);

            var downButton = FindViewById<Button>(Resource.Id.ButtonDown);
            downButton.Click += DownButton_Click;
            var upButton = FindViewById<Button>(Resource.Id.ButtonUp);
            upButton.Click += UpButton_Click;
        }

        void DownButton_Click(object sender, EventArgs e)
        {
            _karmaMeter.KarmaValue -= 0.10d;
        }

        void UpButton_Click(object sender, EventArgs e)
        {
            _karmaMeter.KarmaValue += 0.10d;
        }
    }
}