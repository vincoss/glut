using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PagesSamples.Views
{
    public class CarouselPageSampleTwo : CarouselPage
    {
        public CarouselPageSampleTwo()
        {
            List<ContentPage> pages = new List<ContentPage>(0);
            Color[] colors = { Color.Blue, Color.Red, Color.Green };
            foreach (Color c in colors)
            {
                pages.Add(new ContentPage
                {
                    Content = new StackLayout
                    {
                        Children = {
                new Label { Text = c.ToString () },
                new BoxView {
                    Color = c,
                    VerticalOptions = LayoutOptions.FillAndExpand
                }
            }
                    }
                });
            }

            foreach(var page in pages)
            {
                this.Children.Add(page);
            }
        }
    }
}
