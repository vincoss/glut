using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PagesSamples.Behaviors
{
    public class ToolbarItemsAttachedProperty
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.CreateAttached<ToolbarItemsAttachedProperty, IEnumerable<object>>(
             bindable => ToolbarItemsAttachedProperty.GetItemsSource(bindable),
             null, /* default value */
             BindingMode.Default,
             null,
             (b, o, n) => ToolbarItemsAttachedProperty.OnItemsSourceChanged(b, o, n),
             null,
             null);

        public static IEnumerable<object> GetItemsSource(BindableObject bo)
        {
            return (IEnumerable<object>)bo.GetValue(ToolbarItemsAttachedProperty.ItemsSourceProperty);
        }

        public static void SetItemsSource(BindableObject bo, IEnumerable<object> value)
        {
            bo.SetValue(ToolbarItemsAttachedProperty.ItemsSourceProperty, value);
        }

        public static void OnItemsSourceChanged(BindableObject bo, IEnumerable<object> oldValue, IEnumerable<object> newValue)
        {
            var page = bo as Page;
            if (page == null)
            {
                throw new InvalidOperationException("Only types that inherit from Page type are supported.");
            }

            page.ToolbarItems.Clear();

            foreach (var item in newValue)
            {
                var toolbarMenuItem = new ToolbarItem();
                toolbarMenuItem.BindingContext = item;
                page.ToolbarItems.Add(toolbarMenuItem);
            }
        }
    }

    public class MenuItemViewModel
    {
        public string Name { get; set; }
        public ICommand Command { get; set; }
    }
}
