using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ScrollViewSamples.Views
{
    public partial class HorizontalScrollBindingView : ContentPage
    {
        public HorizontalScrollBindingView()
        {
            InitializeComponent();

            this.BindingContext = new HorizontalScrollBindingViewModel();
        }
    }

    public class HorizontalScrollBindingViewModel
    {
        public HorizontalScrollBindingViewModel()
        {
            this.ItemsSource = new[]
            {
                "#FF141C",
                "#163DFF",
                "#9B19FF",
                "#4CFFFF",
                "#61FF3A",
                "#FFEA30",
                "#FF7D14",
                ""
            };
        }

        public IEnumerable<string> ItemsSource { get; set; }
    }

}
