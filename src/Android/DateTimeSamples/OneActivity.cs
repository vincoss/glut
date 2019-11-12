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
using System.Threading.Tasks;
using System.Threading;

namespace DateTimeSamples
{
    [Activity(Label = "OneActivity")]
    public class OneActivity : Activity
    {
        private DateTime _date;
        private const int DateDialogId = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.OneView);

            // add a click event handler to the button
            ShowPickerImageButton.Click += delegate { ShowDialog(DateDialogId); };

            // get the current date
            _date = DateTime.Today;

            // display the current date (this method is below)
            UpdateDisplay();
        }

        #region View controls implementation

        private TextView _dateDisplayTextView;

        public TextView DateDisplayTextView
        {
            get { return _dateDisplayTextView ?? (_dateDisplayTextView = FindViewById<TextView>(Resource.Id.dateDisplay)); }
        }

        private ImageButton _pickDateButton;

        public ImageButton ShowPickerImageButton
        {
            get { return _pickDateButton ?? (_pickDateButton = FindViewById<ImageButton>(Resource.Id.ShowPickerImageButton)); }
        } 

        #endregion

        // updates the date in the TextView
        private void UpdateDisplay()
        {
            DateDisplayTextView.Text = _date.ToString("d");
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
    }

    [Activity(Label = "OneActivity")]
    public class OneMvvmActivity : Activity
    {
        private const int DateDialogId = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.OneView);

            // add a click event handler to the button
            ShowPickerImageButton.Click += delegate { ShowDialog(DateDialogId); };

            this.Model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Date")
            {
                UpdateDisplay();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            Model.Initialize();
        }

        #region Public properties

        private OneViewModel _model;

        public OneViewModel Model
        {
            get
            {
                return _model ?? (_model = new OneViewModel()
                    {
                        RunOnUiThread = this.RunOnUiThread
                    });
            }
        }

        #endregion

        #region View controls implementation

        private TextView _dateDisplayTextView;

        public TextView DateDisplayTextView
        {
            get { return _dateDisplayTextView ?? (_dateDisplayTextView = FindViewById<TextView>(Resource.Id.dateDisplay)); }
        }

        private ImageButton _pickDateButton;

        public ImageButton ShowPickerImageButton
        {
            get { return _pickDateButton ?? (_pickDateButton = FindViewById<ImageButton>(Resource.Id.ShowPickerImageButton)); }
        }

        #endregion

        // updates the date in the TextView
        private void UpdateDisplay()
        {
            DateDisplayTextView.Text = this.Model.Date.ToString("d");
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
    }

    public class OneViewModel : ViewModelBase
    {
        public override void Initialize()
        {
            if (this.IsInitialized)
            {
                return;
            }

            // Assume that data it loaded from somewhere

            Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(3000);
                    OnLoadingCompleted();
                });
        }

        private void OnLoadingCompleted()
        {
            this.RunOnUiThread(() =>
                {
                    this.Date = DateTime.Today.AddDays(-4);
                    this.IsInitialized = true;
                });
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }
        }
    }
}