using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Xamarin_Validation.Validation
{
    public abstract class ValidationViewModelBase : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private ModelStateDictionary _modelState;

        #region INotifyDataErrorInfo memebers

        public bool HasErrors
        {
            get { return ModelState.IsValid == false; }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (ModelState.ContainsKey(propertyName))
            {
                return ModelState[propertyName];
            }
            return new string[0];
        }

        private void RaiseErrorsChanged(string propertyName)
        {
            var handler = ErrorsChanged;
            if (handler != null)
            {
                handler(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private void ModelStateDictionary_HasErrorsChanged(object sender, PropertyNameEventArgs e)
        {
            RaiseErrorsChanged(e.PropertyName);
            //RaisePropertyChanged("HasErrors");
            //RaisePropertyChanged("Error");
        }

        private ErrorIndexer _error;

        public ErrorIndexer Error
        {
            get { return _error ?? (_error = new ErrorIndexer(this.ModelState)); }
        }

        #endregion

        protected ModelStateDictionary ModelState
        {
            get
            {
                if (_modelState == null)
                {
                    InitializeModelState();
                }
                return _modelState;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void InitializeModelState()
        {
            if (_modelState == null)
            {
                _modelState = new ModelStateDictionary();
                _modelState.HasErrorsChanged += ModelStateDictionary_HasErrorsChanged;
            }
        }


    }

    public class ErrorIndexer
    {
        private readonly IDictionary<string, List<string>> _errors;

        public ErrorIndexer(IDictionary<string, List<string>> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException("errors");
            }
            _errors = errors;
        }

        public string this[string propertyName]
        {
            get
            {
                string error = null;
                if (_errors.ContainsKey(propertyName))
                {
                    var items = _errors[propertyName];
                    error = items.FirstOrDefault();
                }
                return error;
            }
        }
    }
}
