using System.Linq;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Framework;


namespace ServiceSample
{
    [Activity(Label = "Services Samples", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        private readonly MenuService _menuService;

        public MainActivity()
        {
            this._menuService = new MenuService();
            _menuService.Add("Service stocks samples", typeof (StockActivity));
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
