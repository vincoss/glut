using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;


namespace Xamarin_Samples.Extensions
{
    public class ListViewInfiniteScrollAttached
    {
        public static BindableProperty LoadMoreCommandProperty = BindableProperty.CreateAttached("LoadMoreCommand", typeof(ICommand), typeof(Nullable), null, propertyChanged: HandleChanged);

        public static ICommand GetLoadMoreCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(LoadMoreCommandProperty);
        }

        public static void SetLoadMoreCommand(BindableObject view, ICommand cmd)
        {
            view.SetValue(LoadMoreCommandProperty, cmd);
        }

        private static void HandleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as ListView;

            if (control == null)
            {
                return;
            }

            control.ItemAppearing += (sender, e) =>
            {
                var listView = (ListView)sender;
                var items = listView.ItemsSource as IList;

                if (items != null && e.Item == items[items.Count - 1])
                {
                    var command = GetLoadMoreCommand(listView);
                    if (command == null)
                    {
                        return;
                    }
                    command.Execute(null);
                }
            };
        }
    }
}