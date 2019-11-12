using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin_Samples.Extensions
{
    public class SearchBarAttached
    {
        public static BindableProperty TextChangedCommandProperty =
            BindableProperty.CreateAttached("TextChangedCommand", typeof(ICommand), typeof(Nullable), null, propertyChanged: HandleChanged);

        public static ICommand GetTextChangedCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(TextChangedCommandProperty);
        }

        public static void SetTextChangedCommand(BindableObject view, ICommand cmd)
        {
            view.SetValue(TextChangedCommandProperty, cmd);
        }

        static void HandleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var seachBar = bindable as SearchBar;

            if (seachBar == null)
                return;

            seachBar.TextChanged += (sender, e) =>
            {
                var s = sender as SearchBar;
                var command = GetTextChangedCommand(s);

                command.Execute(e.NewTextValue);
            };
        }
    }
}
