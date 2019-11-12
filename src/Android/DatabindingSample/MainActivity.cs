using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Framework;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.ObjectModel;


namespace DatabindingSample
{
    [Activity(Label = "Binding Samples", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        private readonly MenuService _menuService;

        public MainActivity()
        {
            this._menuService = new MenuService();
            _menuService.Add("Basic Binding", typeof (OneActivity))
                        .Add("Basic Validation Binding", typeof (TwoActivity));
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.ListAdapter = new ArrayAdapter<NavigationItem>(this, Resource.Layout.Main, _menuService.Items.ToList());
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var item = _menuService[position];

            var intent = new Intent(this, item.Type);

            StartActivity(intent);
        }
    }
}

