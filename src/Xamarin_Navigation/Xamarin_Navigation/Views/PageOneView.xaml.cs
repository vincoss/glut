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
    public partial class PageOneView : ContentPage
    {
        public PageOneView(DateTime date)
        {
            InitializeComponent();

            // Passing Data when Navigating
            lblDate.Text = date.ToString();
        }
    }
}