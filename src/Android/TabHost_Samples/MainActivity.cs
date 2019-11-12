using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TabHost_Samples
{

    // TabHost and TabWidget sample

    [Activity(Label = "TabHost_Samples", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TabActivity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            CreateTab(typeof(OneTabActivity), "OneTab", "One tab header", Resource.Drawable.ic_tab_one);
            CreateTab(typeof(TwoTabActivity), "TwoTab", "Two tab header", Resource.Drawable.ic_tab_two);
    
        }

        private void CreateTab(Type activityType, string tag, string label, int drawableId)
        {
            var intent = new Intent(this, activityType);
            intent.AddFlags(ActivityFlags.NewTask);

            var spec = TabHost.NewTabSpec(tag);
            var drawableIcon = Resources.GetDrawable(drawableId);
            spec.SetIndicator(label, drawableIcon);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }
    }
}

