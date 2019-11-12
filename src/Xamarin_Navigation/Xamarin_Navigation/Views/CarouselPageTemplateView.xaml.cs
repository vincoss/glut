using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Navigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselPageTemplateView : CarouselPage
    {
        public CarouselPageTemplateView()
        {
            InitializeComponent();

            ItemsSource = new[]
            {
                new CarouselPageTemplateViewModel{ Name  = "One", Color = "Orange"},
                new CarouselPageTemplateViewModel{ Name  = "Two", Color = "Green"},
            };
        }
    }

    public class CarouselPageTemplateViewModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}