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

namespace Android.Framework
{
    public class NavigationItem
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class MenuService
    {
        private readonly IList<NavigationItem> _pages = new List<NavigationItem>();

        public MenuService Add(string name, Type type)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            _pages.Add(new NavigationItem { Name = name, Type = type });
            return this;
        }

        public IEnumerable<NavigationItem> Items
        {
            get { return _pages.AsEnumerable(); }
        }

        public NavigationItem this[int index]
        {
            get
            {
                if (index < 0 || index >= _pages.Count())
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return _pages[index];
            }
        }
    }
}