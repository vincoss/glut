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

namespace HandlingRotationSample
{
    [Activity(Label = "ProgramaticLayoutActivity")]
    public class ProgramaticLayoutActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // create a layout
            var rl = new RelativeLayout(this);

            // set layout parameters
            var layoutParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);
            rl.LayoutParameters = layoutParams;

            // create TextView control
            var tv = new TextView(this);

            // set TextView's LayoutParameters
            tv.LayoutParameters = layoutParams;
            tv.Text = "Programmatic Orientation";

            // add TextView to the layout
            rl.AddView(tv);

            // set the layout as the content view
            SetContentView(rl);
        }
    }
}