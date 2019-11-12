using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;

namespace MvvmLightSample
{
    [Activity(Label = "TwoActivity")]
    public class TwoActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.TwoView);

            this.AddBinding(() => NameEditText.Text, () => Vm.Name);
            this.AddBinding(() => Vm.Name, () => NameEditText.Text);
            
            this.AddBinding(() => Vm.Description, () => DescriptionEditText.Text);
            this.AddBinding(() => Vm.Feedback, () => FeedbackTextView.Text);

            SaveButton.AddCommand("Click", Vm.SaveCommand);
        }

        private TwoViewModel _vm;

        public TwoViewModel Vm
        {
            get { return _vm ?? (_vm = new TwoViewModel()); }
        }

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
    }

    public class TwoViewModel : ViewModelBase
    {
        private string _name;
        private string _description;
        private string _feedback;

        private readonly RelayCommand _saveCommand ;

        public TwoViewModel()
        {
            this._name = "Name here...";
            this._description = "Description here...";

            this._saveCommand = new RelayCommand(OnSave, OnCanSave);
        }

        public void OnSave()
        {
            this.Feedback = string.Format("{0} {1}", this.Name, this.Description);
        }

        public bool OnCanSave()
        {
            return true;
        }

        public string Name
        {
            get { return _name; }
            set { this.Set(() => this.Name, ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set { this.Set(() => this.Description, ref _description, value); }
        }

        public string Feedback
        {
            get { return _feedback; }
            set { this.Set(() => this.Feedback, ref _feedback, value); }
        } 

        public RelayCommand SaveCommand
        {
            get { return _saveCommand; }
        }

    }
}