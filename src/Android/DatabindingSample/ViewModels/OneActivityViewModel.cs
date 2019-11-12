using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.Framework.Commands;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Framework;
using System.ComponentModel;


namespace DatabindingSample.ViewModels
{
    public class OneActivityViewModel : ViewModelBase
    {
        private string _name;
        private string _description;
        private string _feedback;

        public OneActivityViewModel()
        {
            this.SaveCommand = new DelegateCommand(OnSave, OnCanSave);

            this.PropertyChanged += OneActivityViewModel_PropertyChanged;
        }

        private void OneActivityViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                if (string.IsNullOrEmpty(this.Name) == false && this.Name.StartsWith("error"))
                {
                    this.ModelState.AddError(e.PropertyName, "Name is required.");
                }
            }
        }

        public override void Initialize()
        {
            this.Name = "Name here...";
            this.Description = "Description here...";
        }

        private void OnSave()
        {
            this.Feedback = string.Format("{0}-{1}", this.Name, this.Description);
        }

        private bool OnCanSave()
        {
            return true;
        }

        public ICommand SaveCommand { get; private set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public string Feedback
        {
            get { return _feedback; }
            private set
            {
                if (_feedback != value)
                {
                    _feedback = value;
                    OnPropertyChanged("Feedback");
                }
            }
        }
    }
}