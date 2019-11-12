using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.ViewModels;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_HyperlinkLabelSampleView : ContentPage
    {
        public UI_HyperlinkLabelSampleView()
        {
            InitializeComponent();

            BindingContext = new HyperlinkLabelSampleViewModel();
        }
    }
}