using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Android.Framework
{
    public class ModelStateDictionary : IDictionary<string, List<string>>, INotifyPropertyChanged
    {
        private readonly Dictionary<string, List<string>> _stateDictionary = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        public ModelStateDictionary()
        {
        }

        public ModelStateDictionary(ModelStateDictionary dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            foreach (var entry in dictionary)
            {
                this.Add(entry);
            }
        }

        public void Add(KeyValuePair<string, List<string>> pair)
        {
            this.Add(pair.Key, pair.Value ?? new List<string>());
        }

        public void Add(string key, List<string> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            foreach (var item in items)
            {
                this.AddError(key, item);
            }
        }

        public void AddError(string key, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            var messages = GetModelStateForKey(key);
            if (messages.Any(x => string.Equals(x, errorMessage, StringComparison.CurrentCultureIgnoreCase)) == false)
            {
                messages.Add(errorMessage);
            }
            OnHasErrorsChanged(new PropertyNameEventArgs(key));
        }

        public void Clear()
        {
            var keys = this.Keys.ToList();
            foreach (var propertyName in keys)
            {
                this.Remove(propertyName);
                this.OnHasErrorsChanged(new PropertyNameEventArgs(propertyName));
            }
        }

        public bool Contains(KeyValuePair<string, List<string>> pair)
        {
            return this.ContainsKey(pair.Key);
        }

        public bool ContainsKey(string key)
        {
            return _stateDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, List<string>>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public void Merge(ModelStateDictionary dictionary)
        {
            if (dictionary == null)
            {
                return;
            }
            foreach (var entry in dictionary)
            {
                this[entry.Key] = entry.Value ?? new List<string>();
                this.OnHasErrorsChanged(new PropertyNameEventArgs(entry.Key));
            }
        }

        public bool Remove(KeyValuePair<string, List<string>> item)
        {
            return Remove(item.Key);
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            bool result = _stateDictionary.Remove(key);
            if (result)
            {
                OnHasErrorsChanged(new PropertyNameEventArgs(key));
            }
            return result;
        }

        public bool TryGetValue(string key, out List<string> value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            return _stateDictionary.TryGetValue(key, out value);
        }

        #region Private methods

        private List<string> GetModelStateForKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            List<string> modelState;
            if (TryGetValue(key, out modelState) == false)
            {
                modelState = new List<string>();
                _stateDictionary[key] = modelState;
            }
            return modelState;
        }

        private void OnHasErrorsChanged(PropertyNameEventArgs e)
        {
            var handler = this.HasErrorsChanged;
            if (handler != null)
            {
                handler(this, e);
            }
            OnPropertyChange("IsValid");
        }

        protected virtual void OnPropertyChange(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_stateDictionary).GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string, List<string>>> GetEnumerator()
        {
            return _stateDictionary.GetEnumerator();
        }

        #endregion

        #region Properties

        public int Count
        {
            get { return _stateDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<string, List<string>>)_stateDictionary).IsReadOnly; }
        }

        public bool IsValid
        {
            get { return Values.All(x => x.Count == 0); }
        }

        public ICollection<string> Keys
        {
            get { return _stateDictionary.Keys; }
        }

        public ICollection<List<string>> Values
        {
            get { return _stateDictionary.Values; }
        }

        public List<string> this[string key]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return new List<string>();
                }
                List<string> value;
                _stateDictionary.TryGetValue(key, out value);
                return value;
            }
            set { _stateDictionary[key] = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyNameErrorEventHandler HasErrorsChanged;

        #endregion
    }

    public class PropertyNameEventArgs : EventArgs
    {
        public PropertyNameEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }

    public delegate void PropertyNameErrorEventHandler(object sender, PropertyNameEventArgs args);
}
