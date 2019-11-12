using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace ScrollViewSamples.Behaviours
{
    // TODO: Remove
    public class StackLayoutAttachedProperty
    {
        //BindableObject

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.CreateAttached<StackLayoutAttachedProperty, IEnumerable<object>>(
              bindable => StackLayoutAttachedProperty.GetItemsSource(bindable),
              null, /* default value */
              BindingMode.Default,
              null,
              (b, o, n) => StackLayoutAttachedProperty.OnItemsSourceChanged(b, o, n),
              null,
              null);

        public static IEnumerable<object> GetItemsSource(BindableObject bo)
        {
            return (IEnumerable<object>)bo.GetValue(StackLayoutAttachedProperty.ItemsSourceProperty);
        }

        public static void SetItemsSource(BindableObject bo, IEnumerable<object> value)
        {
            bo.SetValue(StackLayoutAttachedProperty.ItemsSourceProperty, value);
        }

        public static void OnItemsSourceChanged(BindableObject bo, IEnumerable<object> oldValue, IEnumerable<object> newValue)
        {
            var layout = bo as Layout<View>;
            if(layout == null)
            {
                throw new InvalidOperationException("Only types that inherit from Layout<View> type are supported. Such as StackLayout control");
            }

            layout.Children.Clear();

            foreach(var item in newValue)
            {
                View v = null;
                v.BindingContext = item;
                layout.Children.Add(v);
            }
        }
    }

}
