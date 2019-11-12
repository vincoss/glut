using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using Xamarin.Forms;

namespace Xamarin_Localization.Globalization
{
    public class LocalizedResources : INotifyPropertyChanged
    {
        private const string DEFAULT_LANGUAGE = "en";
        private readonly ResourceManager _resourceManager;
        private CultureInfo _currentCultureInfo;

        public LocalizedResources(Type resource, string language = null) : this(resource, new CultureInfo(language ?? DEFAULT_LANGUAGE))
        { }

        public LocalizedResources(Type resource, CultureInfo cultureInfo)
        {
            _currentCultureInfo = cultureInfo;
            _resourceManager = new ResourceManager(resource);

            MessagingCenter.Subscribe<object, CultureChangedMessage>(this, string.Empty, OnCultureChanged);
        }

        private void OnCultureChanged(object s, CultureChangedMessage ccm)
        {
            _currentCultureInfo = ccm.NewCultureInfo;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string this[string key]
        {
            get
            {
                if(string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var value = _resourceManager.GetString(key, _currentCultureInfo); // TODO: if not found check compass or samples

                if(string.IsNullOrWhiteSpace(value))
                {
                    return key;
                }

                return value;
            }
        }

    }
}