using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Framework;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DateTimeSamples
{
    [Activity(Label = "ThreeActivity")]
    public class ThreeActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.ThreeView);

            // Add a click listener to the button
            TimePickButton.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);

            // Get the current time
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;

            // Display the current date
            UpdateDisplay();
        }

        private int hour;
        private int minute;

        private const int TIME_DIALOG_ID = 0;

        // Updates the time we display in the TextView
        private void UpdateDisplay()
        {
            string time = string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0'));
            DisplayTimeTextView.Text = time;
        }

        private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            hour = e.HourOfDay;
            minute = e.Minute;
            UpdateDisplay();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            if (id == TIME_DIALOG_ID)
            {
                return new TimePickerDialog(this, TimePickerCallback, hour, minute, false);
            }
            return null;
        }

        #region View controls implementation

        private Button _timePickButton;

        public Button TimePickButton
        {
            get { return _timePickButton ?? (_timePickButton = FindViewById<Button>(Resource.Id.pickTime)); }
        }

        private TextView _displayTimeTextView;

        public TextView DisplayTimeTextView
        {
            get { return _displayTimeTextView ?? (_displayTimeTextView = FindViewById<TextView>(Resource.Id.timeDisplay)); }
        }

        #endregion
    }

    [Activity(Label = "ThreeActivity")]
    public class ThreeMvvmActivity : Activity
    {
        private const int TimeDialogId = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.ThreeView);

            // Add a click listener to the button
            TimePickButton.Click += (o, e) => ShowDialog(TimeDialogId);

            // Display the current date
            UpdateDisplay();
        }

        // Updates the time we display in the TextView
        private void UpdateDisplay()
        {
            string time = string.Format("{0}:{1}", this.Model.Hour, this.Model.Minute.ToString().PadLeft(2, '0'));
            DisplayTimeTextView.Text = time;
        }

        private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            this.Model.Hour = e.HourOfDay;
            this.Model.Minute = e.Minute;

            UpdateDisplay();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            if (id == TimeDialogId)
            {
                return new TimePickerDialog(this, TimePickerCallback, this.Model.Hour, this.Model.Minute, false);
            }
            return null;
        }

        #region Public properties

        private ThreeViewModel _model;

        public ThreeViewModel Model
        {
            get { return _model ?? (_model = new ThreeViewModel()); }
        } 

        #endregion

        #region View controls implementation

        private Button _timePickButton;

        public Button TimePickButton
        {
            get { return _timePickButton ?? (_timePickButton = FindViewById<Button>(Resource.Id.pickTime)); }
        }

        private TextView _displayTimeTextView;

        public TextView DisplayTimeTextView
        {
            get { return _displayTimeTextView ?? (_displayTimeTextView = FindViewById<TextView>(Resource.Id.timeDisplay)); }
        }

        #endregion
    }

    public class ThreeViewModel : ViewModelBase
    {
        public ThreeViewModel()
        {
            Hour = DateTime.Now.Hour;
            Minute = DateTime.Now.Minute;
        }

        public int Hour { get; set; }

        public int Minute { get; set; }
    }
}