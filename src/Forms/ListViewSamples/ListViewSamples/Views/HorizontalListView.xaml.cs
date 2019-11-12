using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ListViewSamples.Views
{
    public partial class HorizontalListView : ContentPage
    {
        public HorizontalListView()
        {
            InitializeComponent();

            this.BindingContext = new HorizontalListViewModel();
        }
    }

    public class HorizontalListViewModel
    {
        public HorizontalListViewModel()
        {
            this.ItemsSource = new[]
            {
                "Red",
                "Blue",
                "Green"
            };
        }

        public IEnumerable<string> ItemsSource { get; set; }
    }
}
