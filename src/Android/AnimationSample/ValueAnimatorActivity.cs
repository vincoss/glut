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
using Android.Animation;

namespace AnimationSample
{
    [Activity(Label = "ValueAnimatorActivity")]
    public class ValueAnimatorActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ValueAnimator);

            var seekBar = FindViewById<SeekBar>(Resource.Id.SeekBarValueAnimator);

            ValueAnimator colorAnim = ObjectAnimator.OfInt(seekBar, "progress", 0, 100);
            colorAnim.SetDuration(1000);
            colorAnim.Start();
        }
    }
}