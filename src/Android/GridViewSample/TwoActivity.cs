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
    [Activity(Label = "TwoActivity")]
    public class TwoActivity : Activity
    {
        private GridView _gridView;

        private static String[] MOBILE_OS = new String[] {"Android", "iOS", "Windows", "Blackberry"};

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.TwoView);

            _gridView = FindViewById<GridView>(Resource.Id.gridView1);

            _gridView.Adapter = new ImageAdapter(this, MOBILE_OS);

            _gridView.ItemClick += _gridView_ItemClick;
        }

        private void _gridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var textView = e.View.FindViewById<TextView>(Resource.Id.grid_item_label);

            Toast.MakeText(this.ApplicationContext, textView.Text, ToastLength.Short).Show();

            StartActivity(e.Position);
        }

        private void StartActivity(int position)
        {
            // Sending image id to FullScreenActivity
            Intent i = new Intent(this, typeof (TwoItemFullScreenActivity));
            i.PutExtra("Id", position);
            StartActivity(i);
        }
    }

    public class ImageAdapter : BaseAdapter
    {
        private Context context;
	    private  String[] mobileValues;

        public ImageAdapter(Context context, String[] mobileValues)
        {
            this.context = context;
            this.mobileValues = mobileValues;
        }

        public override int Count
        {
            get { return mobileValues.Length; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

            View gridView;

            if (convertView == null)
            {

                gridView = new View(context);

                // get layout from mobile.xml
                gridView = inflater.Inflate(Resource.Layout.TwoItemView, null);

                // set value into textview
                TextView textView = gridView.FindViewById<TextView>(Resource.Id.grid_item_label);
                textView.Text = mobileValues[position];

                // set image based on selected text
                ImageView imageView = gridView.FindViewById<ImageView>(Resource.Id.grid_item_image);

                String mobile = mobileValues[position];

                if (mobile.Equals("Windows"))
                {
                    imageView.SetImageResource(Resource.Drawable.windows_logo);
                }
                else if (mobile.Equals("iOS"))
                {
                    imageView.SetImageResource(Resource.Drawable.ios_logo);
                }
                else if (mobile.Equals("Blackberry"))
                {
                    imageView.SetImageResource(Resource.Drawable.blackberry_logo);
                }
                else
                {
                    imageView.SetImageResource(Resource.Drawable.android_logo);
                }

            }
            else
            {
                gridView = (View)convertView;
            }

            return gridView;
        }
    }
}