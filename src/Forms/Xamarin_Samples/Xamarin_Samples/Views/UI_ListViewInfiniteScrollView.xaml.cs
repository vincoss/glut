using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.ViewModels;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_ListViewInfiniteScrollView : ContentPage
    {
        public UI_ListViewInfiniteScrollView()
        {
            InitializeComponent();

            var model = new UI_ListViewInfiniteScrollViewModel();
            BindingContext = model;
            model.Initialize();
        }
    }
}