using System;
using System.ComponentModel;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Framework;

using DatabindingSample.ViewModels;


namespace DatabindingSample
{
    [Activity(Label = "Basic Lost Focus validation")]
    public class TwoActivity : Activity
    {
        private OneActivityViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.TwoView);

            this.Model.PropertyChanged += Model_PropertyChanged;
            this.Model.ModelState.HasErrorsChanged += ModelState_HasErrorsChanged;

            this.NameEditText.FocusChange += EditText_FocusChange;
            this.DescriptionEditText.FocusChange += EditText_FocusChange;
            this.SaveButton.Click += SaveButton_Click;
        }

        protected override void OnStart()
        {
            base.OnStart();

            this.Model.Initialize();
        }

        #region Control event handlers

        private void EditText_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            var element = (View)sender;
            if (element.HasFocus == false)
            {
                if (element.Id == Resource.Id.Name)
                {
                    this.Model.Name = this.NameEditText.Text;
                }

                if (element.Id == Resource.Id.EditTextDescription)
                {
                    this.Model.Description = this.DescriptionEditText.Text;
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Model.SaveCommand.CanExecute(null))
            {
                Model.SaveCommand.Execute(null);
            }
        }

        #endregion

        #region Model event handlers

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

        private void ModelState_HasErrorsChanged(object sender, PropertyNameEventArgs args)
        {
            this.ApplyControlError(args.PropertyName, (ModelStateDictionary)sender);
        } 

        #endregion

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
            get { return _nameEditText ?? (_nameEditText = FindViewById<EditText>(Resource.Id.Name)); }
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