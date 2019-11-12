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
    public partial class TabbedPageTemplateView : TabbedPage
    {
        public TabbedPageTemplateView()
        {
            InitializeComponent();

            BindingContext = new TabbedPageTemplateViewModel();
        }
    }

    public class TabbedPageTemplateViewModel
    {
        public TabbedPageTemplateViewModel()
        {
            Name = "Hammer & Sickle";
        }

        public string Name { get; set; }
    }

}