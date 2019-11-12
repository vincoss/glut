using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_Navigation.Interfaces;
using Xamarin_Navigation.Models;
using Xamarin_Navigation.Services;
using Xamarin_Navigation.Views;


namespace Xamarin_Navigation.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;

        public SettingViewModel(INavigator navigator)
        {
            _navigator = navigator;

            Items = new List<SettingViewItem>
            {
                new SettingViewItem { PageType = typeof(SettingOneView), Title = "One", Detail = "This is a detail", Icon = "Red" },
                new SettingViewItem { PageType = typeof(SettingTwoView), Title = "Two", Detail = "This is a detail", Icon = "Green" },
                new SettingViewItem { PageType = typeof(string), Title = "Missing", Detail = "This is a detail", Icon = "Pink" }
            };

            PropertyChanged += SettingViewModel_PropertyChanged;
        }

        private void SettingViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Utility.GetPropertyName(() => SelectedItem))
            {
                OnSelectedItemChange();
            }
        }

        private async void OnSelectedItemChange()
        {
            if (SelectedItem == null)
            {
                return;
            }

           await _navigator.PushModalAsync(SelectedItem.PageType, null);
        }

        private SettingViewItem _settingViewItem;
        public SettingViewItem SelectedItem
        {
            get { return _settingViewItem; }
            set { SetProperty(ref _settingViewItem, value); }
        }

        public IEnumerable<SettingViewItem> Items { get; set; }
    }
}