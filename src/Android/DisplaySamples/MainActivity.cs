using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.Text;

namespace DisplaySamples
{
    [Activity(Label = "DisplaySamples", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var detail = FindViewById<EditText>(Resource.Id.detailText);

            DisplayMetrics metrics = new DisplayMetrics();

            this.WindowManager.DefaultDisplay.GetMetrics(metrics);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Density        : {0}", metrics.Density));
            sb.AppendLine(string.Format("DensityDpi     : {0}", metrics.DensityDpi));
            sb.AppendLine(string.Format("Height pixels  : {0}", metrics.HeightPixels));
            sb.AppendLine(string.Format("Width pixels   : {0}", metrics.WidthPixels));
            sb.AppendLine(string.Format("Scaled density : {0}", metrics.ScaledDensity));
            sb.AppendLine(string.Format("Xdpi           : {0}", metrics.Xdpi));
            sb.AppendLine(string.Format("Ydpi           : {0}", metrics.Ydpi));

            detail.Text = sb.ToString();
        }
    }
}

