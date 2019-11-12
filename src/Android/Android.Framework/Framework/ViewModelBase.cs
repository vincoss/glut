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

namespace Android.Framework
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public virtual void Initialize()
        {

        }

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool IsInitialized { get; set; }

        public Action<Action> RunOnUiThread;

        private ModelStateDictionary _modelState;

        public ModelStateDictionary ModelState
        {
            get { return _modelState ?? (_modelState = new ModelStateDictionary()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}