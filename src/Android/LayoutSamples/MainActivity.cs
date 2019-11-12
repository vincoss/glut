using System;
using System.Linq;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Framework;

namespace LinearAndRelativeLayoutSample
{
    [Activity(Label = "Layout Samples", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        private readonly MenuService _menuService;

        public MainActivity()
        {
            this._menuService = new MenuService();
            _menuService.Add("Linear Layout", typeof (LinearLayoutActivity))
                        .Add("RelativeLayout", typeof (RelativeLayoutActivity))
                        .Add("Relative Layout One", typeof (RelativeLayoutOneActivity))
                        .Add("Frame Layout", typeof (FrameLayoutActivity))
                        .Add("Complex Relative Layout", typeof (ComplexRelativeLayout))
                        .Add("Custom Layout", typeof (CustomLayoutActivity))
                        .Add("Table Layout", typeof (TableLayoutActivity))
                        .Add("Programatically Table Layout", typeof (ProgramaticTableLayoutActivity))
                        .Add("Dynamic Table Layout", typeof (DynamicTableActivity))
                        .Add("GrdiView Layout", typeof (GridViewActivity));
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

