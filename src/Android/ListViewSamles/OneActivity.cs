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

namespace ListViewSamles
{
    [Activity(Label = "OneActivity")]
    public class OneActivity : Activity
    {
        private ListView _listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.OneView);

            _listView = FindViewById<ListView>(Resource.Id.ListViewRoot); // get reference to the ListView in the layout

            // NOTE: Must be set to ensure that selected row backcolor works.
            _listView.ChoiceMode = ChoiceMode.Multiple;

            // populate the listview with data
            _listView.Adapter = new HomeScreenAdapter(this, TableItem.GetItems().ToList());
            _listView.ItemClick += OnListItemClick;  // to be defined
        }

        protected void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = TableItem.GetItems()[e.Position];
            Android.Widget.Toast.MakeText(this, t.Name, Android.Widget.ToastLength.Short).Show();
        }
    }

    public class TableItem
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int ImageId { get; set; }

        public static IList<TableItem> GetItems()
        {
            var items = new List<TableItem>();

            items.Add(new TableItem {Name = "One", Description = "One description", ImageId = Resource.Drawable.Icon});
            items.Add(new TableItem { Name = "Two", Description = "Two description", ImageId = Resource.Drawable.Icon });
            items.Add(new TableItem { Name = "Three", Description = "Three description", ImageId = Resource.Drawable.Icon });
            items.Add(new TableItem { Name = "Four", Description = "Four description", ImageId = Resource.Drawable.Icon });
            items.Add(new TableItem { Name = "Five", Description = "Five description", ImageId = Resource.Drawable.Icon });
            items.Add(new TableItem { Name = "Six", Description = "Six description", ImageId = Resource.Drawable.Icon });
            items.Add(new TableItem { Name = "Seven", Description = "Seven description", ImageId = Resource.Drawable.Icon });
            items.Add(new TableItem { Name = "Eight", Description = "Eight description", ImageId = Resource.Drawable.Icon });
            items.Add(new TableItem { Name = "Nine", Description = "Nine description", ImageId = Resource.Drawable.Icon });

            return items;
        }
    }

    public class HomeScreenAdapter : BaseAdapter<TableItem>
    {
        private List<TableItem> _items;
        private Activity _context;

        public HomeScreenAdapter(Activity context, List<TableItem> items)
            : base()
        {
            this._context = context;
            this._items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override TableItem this[int position]
        {
            get { return _items[position]; }
        }
        public override int Count
        {
            get { return _items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = _context.LayoutInflater.Inflate(Resource.Layout.OneListViewItem, null);
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.Description;
            view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.ImageId);
            return view;
        }
    }
}