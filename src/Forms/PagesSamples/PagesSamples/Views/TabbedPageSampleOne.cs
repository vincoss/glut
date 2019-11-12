using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace PagesSamples.Views
{
    public class TabbedPageSampleOne : TabbedPage
    {
        public TabbedPageSampleOne()
        {
            this.Title = "TabbedPage";

            this.ItemsSource = new NamedColor[] 
            {
                new NamedColor ("Red", Color.Red),
                new NamedColor ("Yellow", Color.Yellow),
                new NamedColor ("Green", Color.Green),
                new NamedColor ("Aqua", Color.Aqua),
                new NamedColor ("Blue", Color.Blue),
                new NamedColor ("Purple", Color.Purple)
            };

            this.ItemTemplate = new DataTemplate (() => { 
                return new NamedColorPage (); 
            });
        }
    }

    // Data type:
    class NamedColor
    {
        public NamedColor(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
        }

        public string Name { private set; get; }

        public Color Color { private set; get; }

        public override string ToString()
        {
            return Name;
        }
    }

    // Format page
    class NamedColorPage : ContentPage
    {
        public NamedColorPage()
        {
            // This binding is necessary to label the tabs in
            // the TabbedPage.
            this.SetBinding(ContentPage.TitleProperty, "Name");
            // BoxView to show the color.
            BoxView boxView = new BoxView
            {
                WidthRequest = 100,
                HeightRequest = 100,
                HorizontalOptions = LayoutOptions.Center
            };
            boxView.SetBinding(BoxView.ColorProperty, "Color");

            // Build the page
            this.Content = boxView;
        }
    }
}
