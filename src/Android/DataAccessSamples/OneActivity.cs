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
using DataAccessSamples.Data;

namespace DataAccessSamples
{
    [Activity(Label = "OneActivity")]
    public class OneActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.OneView);

            this.AdoButton.Click += AdoButton_Click;
            this.OrmButton.Click += OrmButton_Click;
        }

        #region View controls event handlers

        private void OrmButton_Click(object sender, EventArgs e)
        {
            OutputTextView.Text = OrmExample.DoSomeDataAccess();
        }

        private void AdoButton_Click(object sender, EventArgs e)
        {
            OutputTextView.Text = AdoExample.DoSomeDataAccess();
        } 

        #endregion

        #region View controls implementation

        private Button _adoButton;

        public Button AdoButton
        {
            get { return _adoButton ?? (_adoButton = FindViewById<Button>(Resource.Id.AdoButton)); }
        }

        private Button _ormButton;

        public Button OrmButton
        {
            get { return _ormButton ?? (_ormButton = FindViewById<Button>(Resource.Id.OrmButton)); }
        }

        private TextView _outputTextView;

        public TextView OutputTextView
        {
            get { return _outputTextView ?? (_outputTextView = FindViewById<TextView>(Resource.Id.OutputText)); }
        } 

        #endregion

    }
}