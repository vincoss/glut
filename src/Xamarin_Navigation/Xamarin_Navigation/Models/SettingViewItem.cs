using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Navigation.Models
{
    public class SettingViewItem
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Icon { get; set; }

        public Type PageType { get; set; }
    }
}
