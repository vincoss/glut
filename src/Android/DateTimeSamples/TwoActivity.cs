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
    [Activity(Label = "TwoActivity")]
    public class TwoActivity : Activity
    {
        private DateTime _date;
        private const int DateDialogId = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.TwoView);

            // add a click event handler to the button
            ChangeDateButton.Click += delegate { ShowDialog(DateDialogId); };

            // get the current date
            _date = DateTime.Today;
        }

        // updates the date in the TextView
        private void UpdateDisplay()
        {
            DateOneTextView.Text = _date.ToString("d");
            ResultDatePicker.DateTime = _date;
        }

        // the event received when the user "sets" the date in the dialog
        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            this._date = e.Date;
            UpdateDisplay();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case DateDialogId:
                    {
                        return new DatePickerDialog(this, OnDateSet, _date.Year, _date.Month - 1, _date.Day);
                    }
            }
            return null;
        }

        #region View controls implementation

        private TextView _dateTextView;

        public TextView DateTextView
        {
            get { return _dateTextView ?? (_dateTextView = FindViewById<TextView>(Resource.Id.DateTextView)); }
        }

        private TextView _dateOneTextView;

        public TextView DateOneTextView
        {
            get { return _dateOneTextView ?? (_dateOneTextView = FindViewById<TextView>(Resource.Id.DateOneTextView)); }
        }

        private Button _changeDateButton;

        public Button ChangeDateButton
        {
            get { return _changeDateButton ?? (_changeDateButton = FindViewById<Button>(Resource.Id.ChangeDateButton)); }
        }

        private DatePicker _resultDatePicker;

        public DatePicker ResultDatePicker
        {
            get { return _resultDatePicker ?? (_resultDatePicker = FindViewById<DatePicker>(Resource.Id.ResultDatePicker)); }
        }

        #endregion
    }

    [Activity(Label = "TwoActivity")]
    public class TwoMvvmActivity : Activity
    {
        private const int DateDialogId = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.TwoView);

            // add a click event handler to the button
            ChangeDateButton.Click += delegate { ShowDialog(DateDialogId); };
        }

        // updates the date in the TextView
        private void UpdateDisplay()
        {
            DateOneTextView.Text = this.Model.Date.ToString("d");
            ResultDatePicker.DateTime = this.Model.Date;
        }

        // the event received when the user "sets" the date in the dialog
        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            this.Model.Date = e.Date;

            UpdateDisplay();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case DateDialogId:
                    {
                        return new DatePickerDialog(this, OnDateSet, this.Model.Date.Year, this.Model.Date.Month - 1, this.Model.Date.Day);
                    }
            }
            return null;
        }
        
        #region Public properties

        private TwoViewModel _model;

        public TwoViewModel Model
        {
            get { return _model ?? (_model = new TwoViewModel()); }
        } 

        #endregion

        #region View controls implementation

        private TextView _dateTextView;

        public TextView DateTextView
        {
            get { return _dateTextView ?? (_dateTextView = FindViewById<TextView>(Resource.Id.DateTextView)); }
        }

        private TextView _dateOneTextView;

        public TextView DateOneTextView
        {
            get { return _dateOneTextView ?? (_dateOneTextView = FindViewById<TextView>(Resource.Id.DateOneTextView)); }
        }

        private Button _changeDateButton;

        public Button ChangeDateButton
        {
            get { return _changeDateButton ?? (_changeDateButton = FindViewById<Button>(Resource.Id.ChangeDateButton)); }
        }

        private DatePicker _resultDatePicker;

        public DatePicker ResultDatePicker
        {
            get { return _resultDatePicker ?? (_resultDatePicker = FindViewById<DatePicker>(Resource.Id.ResultDatePicker)); }
        }

        #endregion
    }

    public class TwoViewModel : ViewModelBase
    {
        public TwoViewModel()
        {
            this.Date = DateTime.Today;
        }

        public DateTime Date { get; set; }
    }
}