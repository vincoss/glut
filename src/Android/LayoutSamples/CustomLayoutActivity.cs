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
using LinearAndRelativeLayoutSample.Controls;

namespace LinearAndRelativeLayoutSample
{
    [Activity(Label = "CustomLayoutActivity")]
    public class CustomLayoutActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ViewGroup vg = new CustomLayout(this);
            SetContentView(vg);
        }
    }
}