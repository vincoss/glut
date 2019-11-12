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
    [Activity(Label = "@string/PropertyAnimation", Theme = "@android:style/Theme.Holo.Light.DarkActionBar")]
    public class PropertyAnimationActivity : Activity
    {
        private KarmaMeter _karmaMeter;
        private SeekBar _seekBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.PropertyAnimation);

            _seekBar = FindViewById<SeekBar>(Resource.Id.KarmaSeeker);
            _seekBar.StopTrackingTouch += (sender, args) =>
            {
                double karmaValue = ((double)_seekBar.Progress) / _seekBar.Max;
                _karmaMeter.SetKarmaValue(karmaValue, true);
            };
            _karmaMeter = FindViewById<KarmaMeter>(Resource.Id.KarmaMeter);
        }
    }
}