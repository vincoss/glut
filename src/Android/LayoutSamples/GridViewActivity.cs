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

namespace LinearAndRelativeLayoutSample
{
    [Activity(Label = "GridViewActivity")]
    public class GridViewActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.GridViewLayout);

            var gridView = this.FindViewById<GridView>(Resource.Id.GridViewRoot);
            gridView.Adapter = new ImageAdapter(this);
            gridView.ItemClick += gridView_ItemClick;
        }

        private void gridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, e.Position.ToString(), ToastLength.Short).Show();
        }

        public class ImageAdapter : BaseAdapter
        {
            private Context _context;

            public ImageAdapter(Context c)
            {
                _context = c;
            }

            public override int Count
            {
                get { return _thumbIds.Length; }
            }

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
                ImageView imageView;

                if (convertView == null)
                {
                    // if it's not recycled, initialize some attributes
                    imageView = new ImageView(_context);
                    imageView.LayoutParameters = new GridView.LayoutParams(85, 85);
                    imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                    imageView.SetPadding(8, 8, 8, 8);
                }
                else
                {
                    imageView = (ImageView) convertView;
                }

                imageView.SetImageResource(_thumbIds[position]);
                return imageView;
            }

            // references to our images
            private int[] _thumbIds =
                {
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2,
                    Resource.Drawable.Icon, Resource.Drawable.thumb2
                };
        }
    }
}