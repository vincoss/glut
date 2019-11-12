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
    [Activity(Label = "OneActivity")]
    public class OneActivity : Activity
    {
       private GridView _gridView;

        private static string[] _numbers = new string[]
            {
                "A", "B", "C", "D", "E",
                "F", "G", "H", "I", "J",
                "K", "L", "M", "N", "O",
                "P", "Q", "R", "S", "T",
                "U", "V", "W", "X", "Y", "Z"
            };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.OneView);

            _gridView = FindViewById<GridView>(Resource.Id.gridView1);

            _gridView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, _numbers);

            _gridView.ItemClick += _gridView_ItemClick;
        }

        private void _gridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this.ApplicationContext, ((TextView)e.View).Text, ToastLength.Short).Show();
        }
    }
}