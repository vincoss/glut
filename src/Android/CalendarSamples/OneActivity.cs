using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CalendarSamples
{
    
    [Activity(Label = "OneActivity")]
    public class OneActivity : ListActivity
    {
        private ICursor _cursor;
        private string[] _calendarsProjection;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.OneView);

            // List Calendars
            var calendarsUri = CalendarContract.Calendars.ContentUri;

            // Select columns names to be searched

            _calendarsProjection = new[]
                {
                    CalendarContract.Calendars.InterfaceConsts.Id,
                    CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
                    CalendarContract.Calendars.InterfaceConsts.AccountName
                };

            // Create database query

            _cursor = ManagedQuery(calendarsUri, _calendarsProjection, null, null, null);

            // Select columns names to be searched

            string[] sourceColumns =
                {
                    CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
                    CalendarContract.Calendars.InterfaceConsts.AccountName
                };

            // Select columns names to be searched

            int[] targetResources =
                {
                    Resource.Id.calDisplayName, Resource.Id.calAccountName
                };

            // List adapter
            var adapter = new SimpleCursorAdapter(this, Resource.Layout.OneListItem, _cursor, sourceColumns, targetResources);

            ListAdapter = adapter;

            ListView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _cursor.MoveToPosition(e.Position);

            int calId = _cursor.GetInt(_cursor.GetColumnIndex(_calendarsProjection[0]));

            var showEvents = new Intent(this, typeof(TwoActivity));
            showEvents.PutExtra("calId", calId);
            StartActivity(showEvents);
        }
    }
}