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


namespace GridViewSample
{
    [Activity(Label = "TwoItemFullScreenActivity")]
    public class TwoItemFullScreenActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.TwoItemFullScreenView);

            // get intent data
            Intent i = this.Intent;

            // Selected image id
            int position = i.GetIntExtra("Id", 0);
            var imageAdapter = new ImageAdapter(this, new[] {position.ToString()});

            ImageView imageView = FindViewById<ImageView>(Resource.Id.full_image_view);

            var what = imageAdapter.GetItemId(position);

            imageView.SetImageResource((int)what);
        }
    }
}