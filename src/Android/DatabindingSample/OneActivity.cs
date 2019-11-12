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
using System.ComponentModel;
using System.Threading;
using Android.Framework;
using DatabindingSample.ViewModels;


namespace DatabindingSample
{
    [Activity(Label = "Basic Binding")]
    public class OneActivity : Activity
    {
        private OneActivityViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.OneView);

            this.Model.PropertyChanged += Model_PropertyChanged;

            this.NameEditText.FocusChange += EditText_FocusChange;
            this.DescriptionEditText.FocusChange += EditText_FocusChange;
            this.SaveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Model.SaveCommand.CanExecute(null))
            {
                Model.SaveCommand.Execute(null);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            this.Model.Initialize();
        }

        #region Control event handlers
		
        private void EditText_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            var element = (View) sender;
            if (element.HasFocus == false)
            {
                if (element.Id == Resource.Id.EditTextDescription)
                {
                    this.Model.Name = this.NameEditText.Text;
                }

                if (element.Id == Resource.Id.EditTextDescription)
                {
                    this.Model.Description = this.DescriptionEditText.Text;    
                }
            }
        }

        #endregion

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                this.NameEditText.Text = Model.Name;
            }
            if (e.PropertyName == "Description")
            {
                this.DescriptionEditText.Text = Model.Description;
            }
            if (e.PropertyName == "Feedback")
            {
                this.FeedbackTextView.Text = this.Model.Feedback;
            }
        }

        public OneActivityViewModel Model
        {
            get { return _viewModel ?? (_viewModel = new OneActivityViewModel()); }
        }

        #region Controls implementation

        private Button _saveButton;

        public Button SaveButton
        {
            get { return _saveButton ?? (_saveButton = FindViewById<Button>(Resource.Id.ButtonSave)); }
        }

        private EditText _nameEditText;

        public EditText NameEditText
        {
            get { return _nameEditText ?? (_nameEditText = FindViewById<EditText>(Resource.Id.EditTextName)); }
        }

        private EditText _descriptionEditText;

        public EditText DescriptionEditText
        {
            get { return _descriptionEditText ?? (_descriptionEditText = FindViewById<EditText>(Resource.Id.EditTextDescription)); }
        }

        private TextView _feedbackTextView;

        public TextView FeedbackTextView
        {
            get { return _feedbackTextView ?? (_feedbackTextView = FindViewById<TextView>(Resource.Id.FeedbackTextView)); }
        } 

        #endregion
    }
}