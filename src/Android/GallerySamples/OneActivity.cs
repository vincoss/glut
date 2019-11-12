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


namespace GallerySamples
{
    [Activity(Label = "OneActivity")]
    public class OneActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.OneView);

            this.Gallery.Adapter = new ImageAdapter(this);
            this.Gallery.ItemClick += Gallery_ItemClick;
        }

        private void Gallery_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, e.Position.ToString(), ToastLength.Short).Show();
        }

        private Gallery _gallery;

        public Gallery Gallery
        {
            get { return _gallery ?? (_gallery = FindViewById<Gallery>(Resource.Id.gallery)); }
        }

        public class ImageAdapter : BaseAdapter
        {
            private readonly Context _context;

            public ImageAdapter(Context c)
            {
                _context = c;
            }

            public override int Count { get { return _thumbIds.Length; } }

            public override Java.Lang.Object GetItem(int position)
            {
                return null;
            }

            public override long GetItemId(int position)
            {
                return 0;
            }

            // create a new ImageView for each item referenced by the Adapter
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                ImageView i = new ImageView(_context);

                i.SetImageResource(_thumbIds[position]);
                i.LayoutParameters = new Gallery.LayoutParams(150, 100);
                i.SetScaleType(ImageView.ScaleType.FitXy);

                return i;
            }

            // references to our images
            private int[] _thumbIds =
                {
                    Resource.Drawable.A,
                    Resource.Drawable.B,
                    Resource.Drawable.C,
                    Resource.Drawable.D,
                    Resource.Drawable.E,
                    Resource.Drawable.F,
                    Resource.Drawable.G
                };
        }
    }
}