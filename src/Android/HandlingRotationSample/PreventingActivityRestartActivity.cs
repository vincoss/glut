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
using Android.Content.PM;
using Android.Content.Res;


namespace HandlingRotationSample
{
    [Activity(Label = "PreventingActivityRestartActivity", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class PreventingActivityRestartActivity : Activity
    {
        private TextView _tv;
        private string _timestamp;
        private RelativeLayout.LayoutParams _layoutParamsPortrait;
        private RelativeLayout.LayoutParams _layoutParamsLandscape;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _timestamp = DateTime.Now.ToLongTimeString();

            // create a layout
            var rl = new RelativeLayout(this);
            var layoutParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);
            rl.LayoutParameters = layoutParams;

            // get the initial orientation
            var surfaceOrientation = this.WindowManager.DefaultDisplay.Rotation;

            // create the portrait and landscape layout
            _layoutParamsPortrait = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
            _layoutParamsLandscape = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
            _layoutParamsLandscape.LeftMargin = 100;
            _layoutParamsLandscape.TopMargin = 100;

            // create the TextView an assign the initial layout params
            _tv = new TextView(this);

            if (surfaceOrientation == SurfaceOrientation.Rotation0 || surfaceOrientation == SurfaceOrientation.Rotation180)
            {
                _tv.LayoutParameters = _layoutParamsPortrait;
            }
            else
            {
                _tv.LayoutParameters = _layoutParamsLandscape;
            }

            _tv.Text = "Programmatic layout. Timestamp = " + _timestamp;

            rl.AddView(_tv);

            SetContentView(rl);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            Console.WriteLine("config changed");

            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                _tv.LayoutParameters = _layoutParamsPortrait;
                _tv.Text = "Changed to portrait. Timestamp = " + _timestamp;
            }
            else if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                _tv.LayoutParameters = _layoutParamsLandscape;
                _tv.Text = "Changed to landscape. Timestamp = " + _timestamp;
            }
        }
    }
}