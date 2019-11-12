using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace CalendarSamples
{
    [Activity(Label = "Calendar EventList")]
    public class TwoActivity : ListActivity
    {
        private int _calId;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.TwoView);

            _calId = Intent.GetIntExtra("calId", -1);

            ListEvents();

            InitAddEvent();
        }

        private void ListEvents()
        {
            var eventsUri = CalendarContract.Events.ContentUri;

            // Select columns names to be searched

            string[] eventsProjection =
                {
                    CalendarContract.Events.InterfaceConsts.Id,
                    CalendarContract.Events.InterfaceConsts.Title,
                    CalendarContract.Events.InterfaceConsts.Dtstart
                };

            // Create database query

            var cursor = ManagedQuery(eventsUri, eventsProjection, String.Format("calendar_id={0}", _calId), null,
                                      "dtstart ASC");

            // Select columns names to be searched

            string[] sourceColumns =
                {
                    CalendarContract.Events.InterfaceConsts.Title,
                    CalendarContract.Events.InterfaceConsts.Dtstart
                };

            int[] targetResources = {Resource.Id.eventTitle, Resource.Id.eventStartDate};

            // Create database query

            var adapter = new SimpleCursorAdapter(this, Resource.Layout.TwoListItem, cursor, sourceColumns,
                                                  targetResources);

            adapter.ViewBinder = new ViewBinder();

            ListAdapter = adapter;
        }

        private void InitAddEvent()
        {
            var addSampleEvent = FindViewById<Button>(Resource.Id.addSampleEvent);

            addSampleEvent.Click += (sender, e) =>
                {
                    // Create Event code
                    ContentValues eventValues = new ContentValues();
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, _calId);
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Test Event from M4A");
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "This is an event created from Mono for Android");
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(2011, 12, 15, 10, 0));
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(2011, 12, 15, 11, 0));

                    // GitHub issue #9 : Event start and end times need timezone support.
                    // https://github.com/xamarin/monodroid-samples/issues/9
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

                    var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
                    Console.WriteLine("Uri for new event: {0}", uri);
                };
        }

        private class ViewBinder : Java.Lang.Object, SimpleCursorAdapter.IViewBinder
        {
            public bool SetViewValue(View view, Android.Database.ICursor cursor, int columnIndex)
            {
                if (columnIndex == 2)
                {
                    long ms = cursor.GetLong(columnIndex);

                    DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ms).ToLocalTime();

                    TextView textView = (TextView) view;
                    textView.Text = date.ToLongDateString();

                    return true;
                }
                return false;
            }
        }

        private long GetDateTimeMS(int yr, int month, int day, int hr, int min)
        {
            Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Calendar.DayOfMonth, 15);
            c.Set(Calendar.HourOfDay, hr);
            c.Set(Calendar.Minute, min);
            c.Set(Calendar.Month, Calendar.December);
            c.Set(Calendar.Year, 2011);

            return c.TimeInMillis;
        }
    }
}